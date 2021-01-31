using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Numerics;
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


        int penTrackingResetCounter;
        int penTrackingResetCounterLimitAsOfPenTrackingErrorOffset = 1;
        String msg;
        String msgFromInput;

        int refreshTimerInMiliseconds = 50;//, 10 * 1000 = 10 secs , 100 = ,1 sec
        bool isArtistActive = false;

        private NotifyIcon trayIcon;

        public Form2()
        {

            InitializeComponent();
            CreateTrayMenu();

            LoadWindowPosition();

            this.TopMost = true;


           // this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(Form2_MouseDown);
           // this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(Form2_MouseUp);


            HWnd = this.Handle;
            RegisterTabletDevice();

            CreateArtistActiveTimer();

        }



        private void RegisterTabletDevice()
        {
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
        }

        protected override void OnLoad(EventArgs e)
        {
            


            this.ControlBox = false;
            this.Text = "DFA";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            startingTime = DateTime.Now;
            lastStopTime = DateTime.Now;


            base.OnLoad(e);
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


        public DateTime startingTime;
        public TimeSpan differenceOnStopTimeFromLastStopTime;
        public DateTime lastStopTime;
        public DateTime activatedFullTime;
        public bool UpdatedDifferenceOnStop = false;
        public bool ShowActiveTimeInSeconds = false;

        public float currentMainBarProgress;
        public float maxMainBarProgress;
        public int timeSecToFillMainBar = 5;


        public int graphicalProgressBarUpdateInMiliseconds = 5;


        private void CreateArtistActiveTimer()
        {
            Timer timerArtistActive = new Timer();
            timerArtistActive.Interval = (refreshTimerInMiliseconds);
            timerArtistActive.Tick += new EventHandler(TimerArtistActiveTick);
            timerArtistActive.Start();

            maxMainBarProgress = 5 * 1000 / refreshTimerInMiliseconds;

            Timer timerProgressBarsUpdate = new Timer();
            timerProgressBarsUpdate.Interval = graphicalProgressBarUpdateInMiliseconds;
            timerProgressBarsUpdate.Tick += new EventHandler(TimerUpdateProgressBarsGraphically);
            timerProgressBarsUpdate.Start();
        }

        float currentVisualProgressOfLerp=0;

        private void TimerUpdateProgressBarsGraphically(object sender, EventArgs e)
        {

            currentVisualProgressOfLerp = Lerp(currentVisualProgressOfLerp, currentMainBarProgress, (float)0.1);
            progressBarBottomMost.Value = ToSmoothProgressBarProcentage(currentVisualProgressOfLerp, 0,maxMainBarProgress);
            //ToSmoothProgressBarProcentage(currentMainBarProgress, 0, maxMainBarProgress);



        }


        private void TimerArtistActiveTick(object sender, EventArgs e)
        {

            
            if (!isArtistActive)
            {

                progressBarTopMost.BackColor = Color.FromArgb(221, 44, 0);
                if (currentMainBarProgress > 0)
                    currentMainBarProgress--;

                if (!UpdatedDifferenceOnStop)
                {
                    //differenceOnStopTimeFromLastStopTime = DateTime.Now - lastStopTime;
                    //lastStopTime = DateTime.Now;



                    UpdatedDifferenceOnStop = true;

                    //todo confimation test
                    // activatedFullTime = activatedFullTime + differenceOnStopTimeFromLastStopTime;
                }
                else
                {

                }
            }
            else
            {
                progressBarTopMost.BackColor = Color.FromArgb(178, 255, 89);

                activatedFullTime += TimeSpan.FromMilliseconds(refreshTimerInMiliseconds);

                if (currentMainBarProgress < 0)
                    currentMainBarProgress = 0;
                else if (currentMainBarProgress < maxMainBarProgress)
                    currentMainBarProgress++;
                else
                    currentMainBarProgress = 0;





                if (ShowActiveTimeInSeconds)
                {
                    label2TimeLabelText.Text = activatedFullTime.Second.ToString();
                }
                else
                    label2TimeLabelText.Text = activatedFullTime.ToLongTimeString().ToString();

                label3.Text = msgFromInput;
            }
            Invalidate();

        }





        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message message)
        {
            //Console.WriteLine(m);

            msg = message.Msg.ToString();



            switch (message.Msg)
            {
                case (int)MsgType.WM_INPUT:

                    isArtistActive = true;

                    msgFromInput = message.LParam.ToString();
                    penTrackingResetCounter = 0;


                    break;

                default:
                    penTrackingResetCounter++;

                    if (penTrackingResetCounter > penTrackingResetCounterLimitAsOfPenTrackingErrorOffset)
                    {
                        isArtistActive = false;
                        msgFromInput = "";
                        penTrackingResetCounter = 0;
                    }
                    break;
            }



            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;


            base.WndProc(ref message);

        }


        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();



            base.OnFormClosed(e);

        }

        private int ToSmoothProgressBarProcentage(float current, float min, float max)
        {
            var range = max - min;
            var correctedStartVal = current - min;

            return (int)((correctedStartVal * 10000) / range);
        }

        private float ToProcentage(float current, float min, float max)
        {
            var range = max - min;
            var correctedStartVal = current - min;

            return (correctedStartVal * 100) / range;
        }

        float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat + (secondFloat - firstFloat) * by;
        }

        System.Numerics.Vector2 Lerp(Vector2 firstVector, Vector2 secondVector, float by)
        {
            float retX = Lerp(firstVector.X, secondVector.X, by);
            float retY = Lerp(firstVector.Y, secondVector.Y, by);
            return new Vector2(retX, retY);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FormDrawOver(object sender, DragEventArgs e)
        {
            label5.Text = "drag over";
        }

        private void FormMouseHover(object sender, EventArgs e)
        {
            label5.Text = "FormMouseHover";
            
        }

        private void label5_Click(object sender, EventArgs e)
        {
            label5.Text = "lab5 click " + e.ToString();
            

        }


        private void FormMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);

            }
            label5.Text = "FormMouseDown " + e.Button.ToString();

        }

        private void FormMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            label5.Text = "FormMouseUp " + e.Button.ToString();

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
    }
}
