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

    public partial class MainForm : Form
    {
        

        
        public enum MsgType
        {
            WM_INPUT = 0x00FF

        }
        public int DefaultWindowX => Screen.FromControl(this).WorkingArea.Width / 2;
        public int DefaultWindowY => 0;


        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_NCRBUTTONDOWN = 0xA4;
        public const int WM_RBUTTONDOWN = 0x0204;
        private const int MilestoneCheckingInterval = 300;
        int penTrackingResetCounter = 0;
        int penTrackingResetCounterLimitAsOfPenTrackingErrorOffset = 0;
        String wndProcMsg;
        String msgFromInput;

        int refreshArtistStateTickTimerInMiliseconds = 50;//, 10 * 1000 = 10 secs , 100 = ,1 sec

        public DateTime startingTime;
        public DateTime lastStopTime;


        public TimeSpan activatedFullTime;

        public bool ShowActiveTimeInSeconds = false;



        public int graphicalProgressBarUpdateInMiliseconds = 5;
        private bool receivedInputFromPenOnLastWndProc = false;



        private static ArtistState _currentArtistState = ArtistState.INACTIVE;
        public static ArtistState ArtistState { get => _currentArtistState; set => _currentArtistState = value; }

        public static bool ArtistActive
        {
            get
            {
                return ArtistState == ArtistState.ACTIVE;
            }
            set
            {
                ArtistState = value ? ArtistState.ACTIVE : ArtistState.INACTIVE;
            }
        }


        private NotifyIcon trayIcon;
        public IntPtr HWnd { get; set; }

        public MainForm()
        {
            InitializeComponent();

            CreateTrayMenu();
            LoadWindowPosition();

            this.ControlBox = false;
            this.Text = "DFA";

            startingTime = DateTime.Now;
            lastStopTime = DateTime.Now;

            HWnd = this.Handle;
            RegisterTabletDevice();

            CreateArtistStateTickTimer();

            CreateGraphicalTickTimer();

            milestone = new Milestone();
            CreateMilestoneTimer();

        }

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        }


        private void RegisterTabletDevice()
        {
            RAWINPUTDEVICE rid = new RAWINPUTDEVICE();

            rid.usUsagePage = 0x000D;
            rid.usUsage = 0x0001; //01 for external, 02 for integrated
            rid.dwFlags = 0x00000100;
            rid.hwndTarget = HWnd;

            //if (RegisterRawInputDevices(rid[1], 1, Convert.ToUInt32(Marshal.SizeOf(rid[1]))) == false)
            if (RegisterRawInputDevices(rid, 1, Convert.ToUInt32(Marshal.SizeOf(rid))) == false)
            {
                label1.Text = "registration failed";
            }
            else
            {
                //      label1.Text = "registration success";

            }
        }
        public void CreateTrayMenu()
        {

            trayIcon = new NotifyIcon();
            trayIcon.Icon = new System.Drawing.Icon("Resources/AppIcon.ico");
            trayIcon.Text = "DFA";
            trayIcon.MouseClick += TrayIconMouseClicked;

            trayIcon.ContextMenuStrip = new ContextMenuStrip();
            trayIcon.ContextMenuStrip.Items.Add("Exit", Image.FromFile("Resources/AppIcon.ico"), TrayOnExitClicked);
            trayIcon.ContextMenuStrip.Items.Add(new ToolStripDropDownButton("Settings...", null,
                new ToolStripLabel("Reset position", null, false, TrayResetPosition)
                ));
            trayIcon.Visible = true;
        }
        private void TrayResetPosition(object sender, EventArgs e)
        {
            ResetWindowPosition();
        }
        private void TrayOnExitClicked(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TrayIconMouseClicked(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.TopMost = !this.TopMost;
                if (TopMost)
                    BringToFront();
                else
                    SendToBack();

            }
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


        private void CreateArtistStateTickTimer()
        {
            Timer timerArtistActive = new Timer();
            timerArtistActive.Interval = (refreshArtistStateTickTimerInMiliseconds);
            timerArtistActive.Tick += new EventHandler(TimerArtistStateTick);
            timerArtistActive.Start();
        }


        private void TimerArtistStateTick(object sender, EventArgs e)
        {

            bool changed = CheckArtistStateChanged();



            if (ArtistState == ArtistState.ACTIVE)
                OnArtistStateActiveTick();
            else
                OnArtistStateInactiveTick();

            Invalidate();
        }

        private void OnArtistStateActiveActivated()
        {
            progressBarTopMost.BackColor = Color.FromArgb(178, 255, 89);
        }

        private void OnArtistStateActiveTick()
        {
            activatedFullTime += TimeSpan.FromMilliseconds(refreshArtistStateTickTimerInMiliseconds);


            DisplayTimeActivatedInUI();

        }

        private void DisplayTimeActivatedInUI()
        {
            if (ShowActiveTimeInSeconds)
            {
                label2TimeLabelText.Text = activatedFullTime.TotalSeconds.ToString().TrimEnd('0', ' ');
            }
            else
            {

                StringBuilder sb = new StringBuilder(activatedFullTime.ToString());
                sb.Truncate(11);

                if (sb.Length == 8)
                    sb.Append(".00");
                label2TimeLabelText.Text = sb.ToString();

                sb.Clear();
                sb = null;

             
            }
        }

        private void OnArtistStateInactiveActivated()
        {
            progressBarTopMost.BackColor = Color.FromArgb(221, 44, 0);

        }
        private void OnArtistStateInactiveTick()
        {

        }


        private bool CheckArtistStateChanged()
        {
            ArtistState newState = TickTimerGetNewArtistStateBasedOnInput();




            bool changed;

            if (ArtistState == ArtistState.ACTIVE)
                changed = false;
            else
                changed = true;

            ArtistState = newState;


            if (changed)
                if (ArtistState == ArtistState.ACTIVE)
                    OnArtistStateActiveActivated();
                else
                    OnArtistStateInactiveActivated();





            return changed;
        }

        private ArtistState TickTimerGetNewArtistStateBasedOnInput()
        {

            if (Control.ModifierKeys == Keys.Control)
                return ArtistState.ACTIVE;

            if (!receivedInputFromPenOnLastWndProc)
            {
                if (penTrackingResetCounterLimitAsOfPenTrackingErrorOffset > 0)
                {
                    penTrackingResetCounter++;

                    if (penTrackingResetCounter > penTrackingResetCounterLimitAsOfPenTrackingErrorOffset)
                    {

                        return ArtistState.INACTIVE;
                    }
                    else
                        return ArtistState.ACTIVE;
                }
                else
                    return ArtistState.INACTIVE;


            }


            if (receivedInputFromPenOnLastWndProc)
            {
                penTrackingResetCounter = 0;
                return ArtistState.ACTIVE;
            }

            return ArtistState.INACTIVE;

        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message message)
        {
            //Console.WriteLine(m);

            wndProcMsg = message.Msg.ToString();



            switch (message.Msg)
            {
                case (int)MsgType.WM_INPUT:
                    receivedInputFromPenOnLastWndProc = true;

                    break;

                default:
                    receivedInputFromPenOnLastWndProc = false;


                    break;
            }



            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;


            base.WndProc(ref message);

        }

        private void CreateGraphicalTickTimer()
        {
            Timer timerProgressBarsUpdate = new Timer();
            timerProgressBarsUpdate.Interval = graphicalProgressBarUpdateInMiliseconds;
            timerProgressBarsUpdate.Tick += new EventHandler(TimerUpdateProgressBarsGraphically);
            timerProgressBarsUpdate.Start();
        }


        float currentVisualProgressOfLerp = 0;
        float desiredBarValue = 0;
        float percentFilled = 0;
        public int timeSecToFillMainBar = 5;



        private void TimerUpdateProgressBarsGraphically(object sender, EventArgs e)
        {
            if (ArtistActive)
            {

                float rest = (float)(activatedFullTime.TotalSeconds % (timeSecToFillMainBar));
                percentFilled = Utils.ToProcentage(rest, 0, timeSecToFillMainBar);
                desiredBarValue = (int)Utils.ProcentToProgressBarValue(progressBarBottomMost, percentFilled);
            }



            if (currentVisualProgressOfLerp > desiredBarValue)
            {
                currentVisualProgressOfLerp = desiredBarValue;
                progressBarBottomMost.Value = (int)desiredBarValue;
            }

            float lerpSpeed = percentFilled / 100;
            currentVisualProgressOfLerp = Utils.Lerp(currentVisualProgressOfLerp, desiredBarValue, lerpSpeed);

            progressBarBottomMost.Value = (int)currentVisualProgressOfLerp < 0 ? 0 : (int)currentVisualProgressOfLerp;

            //label3.Text = "r" + rest + " %" + percentFilled + " d" + desiredBarValue + "l" + lerpSpeed;

            Invalidate();

        }

        private void CreateMilestoneTimer()
        {

            Timer timerMilestone = new Timer();
            timerMilestone.Interval = MilestoneCheckingInterval;
            timerMilestone.Tick += new EventHandler(TimerMilestone);
            timerMilestone.Start();
        }

        Milestone milestone;
        private void TimerMilestone(object sender, EventArgs e)
        {
            //if (CurrentArtistState == ArtistState.ACTIVE)
            //{
            //    TimeSpan r = milestone.CheckTimespanMilestoneAchieved(TimeSpan.FromHours(activatedFullTime.Hour) + TimeSpan.FromMinutes(activatedFullTime.Minute) + TimeSpan.FromSeconds(activatedFullTime.Second));
            //    if (r > new TimeSpan(0, 0, 1))
            //        label4.Text = milestone.GetAchievedMessage();
            //    label5.Text = r.ToString();
            //}
        }








        public bool isMouseDown = false;
        public bool isLMouseDown = false;
        public bool isRMouseDown = false;

        public int mouseX;
        public int mouseY;
        public int mouseinX;
        public int mouseinY;

        private void FormMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            isMouseDown = true;

            if (e.Button == MouseButtons.Left)
                isLMouseDown = true;

            if (e.Button == MouseButtons.Right)
            {
                isRMouseDown = true;

                mouseinX = MousePosition.X - Bounds.X;
                mouseinY = MousePosition.Y - Bounds.Y;

                //ReleaseCapture();
                //SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);

            }

        }

        private void FormMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isRMouseDown)
            {


                mouseX = MousePosition.X - mouseinX;
                mouseY = MousePosition.Y - mouseinY;

                this.SetDesktopLocation(mouseX, mouseY);

            }
        }


        private void FormMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {


            if (Control.MouseButtons == MouseButtons.None)
                isMouseDown = false;

            if (e.Button == MouseButtons.Left)
                isLMouseDown = false;

            if (e.Button == MouseButtons.Right)
            {
                isRMouseDown = false;
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            trayIcon.Dispose();
            trayIcon.Icon = null;
            trayIcon.Visible = false;

            base.OnFormClosed(e);

        }

    }
}
