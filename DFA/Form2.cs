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

        public IntPtr HWnd { get; set; }


        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public uint dwFlags;
            public IntPtr hwndTarget;
        }


        public const int WM_NCLBUTTONDOWN = 0xA1;
        private Label label4;
        private Label label5;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        private void Form2_Load(object sender, EventArgs e)
        {
            
            label1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            label3.Parent = pictureBox1;

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
                if ( key  == null)
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

        public void ResetWindowPosition()
        {
            StartPosition = FormStartPosition.Manual;


            this.Location = new Point(DefaultWindowX, DefaultWindowY);
        }

        public Form2()
        {
            
            InitializeComponent();
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
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();


        }


        private void timer_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.Second.ToString() + ": " + msg;

            label3.Text = msgFromInput;
            this.InvokePaint(this, new PaintEventArgs(this.CreateGraphics(), this.DisplayRectangle));

        }

        String msg;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        String msgFromInput;

        public enum MsgType
        {
            WM_INPUT = 0x00FF

        }

        int penTrackingResetCounter;
        int penTrackingResetCounterLimit =5;

        

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



            //Label2 = m.Msg.ToString() + DateTime.Now.Second;
            //Label2 =  DateTime.Now.Second.ToString();
            //Label2 = m.Msg.ToString() ;

            //switch(m.Msg)
            //{
            //    case :
            //        break;
            //}


            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1104, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label1.Location = new System.Drawing.Point(519, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label2.Location = new System.Drawing.Point(662, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label3.Location = new System.Drawing.Point(791, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label4.Location = new System.Drawing.Point(910, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label5.Location = new System.Drawing.Point(998, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "label5";
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(1104, 30);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
