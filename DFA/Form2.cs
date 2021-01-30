using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DFA
{

    public partial class Form2 : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern bool RegisterRawInputDevices(RAWINPUTDEVICE pRawInputDevices, uint uiNumDevices, uint cbSize);
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public IntPtr HWnd { get; set; }

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public uint dwFlags;
            public IntPtr hwndTarget;
        }
        public enum MsgType
        {
            WM_INPUT = 0x00FF

        }

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        
        public const int HT_CAPTION = 0x2;
        
        int penTrackingResetCounter;
        int penTrackingResetCounterLimit = 5;
        String msg;
        String msgFromInput;

        private NotifyIcon trayIcon;

        public Form2()
        {

            InitializeComponent();
            CreateTrayMenu();

            LoadWindowPosition();

            this.TopMost = true;


            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(Form2_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(Form2_MouseUp);


            HWnd = this.Handle;


            RAWINPUTDEVICE[] rid = new RAWINPUTDEVICE[2];
            rid[0].usUsagePage = 0x000D;
            rid[0].usUsage = 0x0000; //01 for external, 02 for integrated
            rid[0].dwFlags = 0x00000100;
            rid[0].hwndTarget = HWnd;

            rid[1].usUsagePage = 0x000D;
            rid[1].usUsage = 0x0001; //01 for external, 02 for integrated
            rid[1].dwFlags = 0x00000100;
            rid[1].hwndTarget = HWnd;

            //if (RegisterRawInputDevices(rid[1], 1, Convert.ToUInt32(Marshal.SizeOf(rid[1]))) == false)
            if (RegisterRawInputDevices(rid[1], 1, Convert.ToUInt32(Marshal.SizeOf(rid[1]))) == false)
            {
                label1.Text = "registration failed";
            }
            else
            {
                label1.Text = "registration success";

            }

            Timer timer = new Timer();
            timer.Interval = (1 * 100); // 10 * 1000 = 10 secs
            timer.Tick += new EventHandler(TimerTick);
            timer.Start();


        }



        private void Form2_Load(object sender, EventArgs e)
        {

            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label4.Parent = pictureBox1;
            label5.Parent = pictureBox1;

        }
        protected override void OnLoad(EventArgs e)
        {

            this.ControlBox = false;
            this.Text = String.Empty;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            base.OnLoad(e);

        }


        private void Form2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            }

        }

        private void Form2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RegistryKey key;
                key = Registry.CurrentUser.OpenSubKey("DFA", true);
                if (key == null)
                    key = Registry.CurrentUser.CreateSubKey("DFA", true);

                int x = this.Location.X;
                int y = this.Location.Y;
                key.SetValue("DFAMainWindowPositionX", x);
                key.SetValue("DFAMainWindowPositionY", y);



                key.Close();

            }

        }

        public int DefaultWindowX => Screen.FromControl(this).WorkingArea.Width / 2;
        public int DefaultWindowY => 0;

      

        public void CreateTrayMenu()
        {

            trayIcon = new NotifyIcon();
            trayIcon.Icon = new System.Drawing.Icon("Resources/AppIcon.ico");
            trayIcon.Text = "DFA";
            trayIcon.Click += TrayIconClicked;


            trayIcon.ContextMenuStrip = new ContextMenuStrip();
            trayIcon.ContextMenuStrip.Items.Add("Exit", Image.FromFile("Resources/AppIcon.ico"), OnExitClicked);
            //label button
            trayIcon.ContextMenuStrip.Items.Add(new ToolStripDropDownButton("Settings...", null,
                new ToolStripLabel("Reset position", null, false, TrayResetPosition)
                //,new ToolStripLabel("test2")
                ));






            trayIcon.Visible = true;




        }

       

        private void TrayResetPosition(object sender, EventArgs e)
        {
            ResetWindowPosition();
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void TrayIconClicked(object sender, EventArgs e)
        {
            // MessageBox.Show("cl");
        }


        public void ResetWindowPosition()
        {
            StartPosition = FormStartPosition.Manual;


            this.Location = new Point(DefaultWindowX, DefaultWindowY);
        }

        private void LoadWindowPosition()
        {


            var key = Registry.CurrentUser.OpenSubKey("DFA", true);
            if (key != null)
            {
                int posX = (int)key.GetValue("DFAMainWindowPositionX");
                int posY = (int)key.GetValue("DFAMainWindowPositionY");

                if (posX != 0 && posY != 0)
                {
                    StartPosition = FormStartPosition.Manual;

                    Point point = this.Location;
                    point.X = posX;
                    point.Y = posY;

                    this.Location = point;

                }
            }

        }


        private void TimerTick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.Second.ToString() + ": " + msg;

            label3.Text = msgFromInput;

        }

      

        

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message message)
        {
            //Console.WriteLine(m);

            msg = message.Msg.ToString();





            switch (message.Msg)
            {
                case (int)MsgType.WM_INPUT:

                    msgFromInput = message.LParam.ToString();
                    penTrackingResetCounter = 0;

                    break;

                default:
                    penTrackingResetCounter++;

                    if (penTrackingResetCounter > penTrackingResetCounterLimit)
                    {
                        msgFromInput = "";
                        penTrackingResetCounter = 0;
                    }
                    break;
            }




            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }


        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();



            base.OnFormClosed(e);

        }
    }
}
