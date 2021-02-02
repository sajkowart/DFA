using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DFA.Forms
{

    public partial class MainForm : Form, IMainForm
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
        int penTrackingResetCounter = 0;
        int penTrackingResetCounterLimitAsOfPenTrackingErrorOffset = 0;

        int refreshArtistStateTickTimerInMiliseconds = 6;//, 10 * 1000 = 10 secs , 1000 = ,1 sec

        public DateTime startingTime;
        public DateTime lastStopTime;


        public TimeSpan activatedFullTime;

        public bool ShowActiveTimeInSeconds = false;



        public int graphicalProgressBarUpdateInMiliseconds = 5;
        private bool receivedInputFromPenOnLastWndProc = false;



        private static ArtistState _currentArtistState = ArtistState.INACTIVE;
        public static ArtistState ArtistState { get => _currentArtistState; private set => _currentArtistState = value; }

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
            HWnd = this.Handle;


            ClearLabels();

            CreateTrayMenu();
            LoadWindowPosition();
            LoadDailyGoal();

            this.DoubleBuffered = true;



            startingTime = DateTime.Now;
            lastStopTime = DateTime.Now;

            RegisterTabletDevice();

            CreateArtistStateTickTimer();

           // CreateGraphicalBarsAndTickTimer();



            CreateMilestoneSystem();
            CreateNotificationSystem();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        private void LoadDailyGoal()
        {
            if (DailyGoalForm.GetDailyGoalTimespan(out TimeSpan result))
                SetDailyGoal(result);
        }

        private void ClearLabels()
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "00:00:00.00";
            label4.Text = "Set daily goal!";
            label5.Text = "";
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

            if (WinApi.RegisterRawInputDevices(rid, 1, Convert.ToUInt32(Marshal.SizeOf(rid))) == false)
            {
                label1.Text = "registration failed";
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
            timerArtistActive.Tick += new EventHandler(TimerUpdateProgressBarsGraphically);
            timerArtistActive.Start();
        }


        private void TimerArtistStateTick(object sender, EventArgs e)
        {

            bool changed = CheckArtistStateChanged();



            if (ArtistActive)
                OnArtistStateActiveTick();
            else
                OnArtistStateInactiveTick();

            Refresh();
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
                label3.Text = activatedFullTime.TotalSeconds.ToString().TrimEnd('0', ' ');
            }
            else
            {

                StringBuilder sb = new StringBuilder(activatedFullTime.ToString());
                sb.Truncate(11);

                if (sb.Length == 8)
                    sb.Append(".00");
                label3.Text = sb.ToString();

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


            Invalidate();


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




            switch (message.Msg)
            {
                case (int)MsgType.WM_INPUT:
                    receivedInputFromPenOnLastWndProc = true;

                    break;

                default:
                    receivedInputFromPenOnLastWndProc = false;


                    break;
            }



            

            base.WndProc(ref message);
            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;

        }

        private void CreateGraphicalBarsAndTickTimer()
        {

            progressBarBottomMost.WithLerp = true;

            Timer timerProgressBarsUpdate = new Timer();
            timerProgressBarsUpdate.Interval = graphicalProgressBarUpdateInMiliseconds;
            timerProgressBarsUpdate.Tick += new EventHandler(TimerUpdateProgressBarsGraphically);
            timerProgressBarsUpdate.Start();
        }


        float botCurrentVisualProgressOfLerp = 0;
        float botDesiredBarValue = 0;
        float botPercentFilled = 0;
        public int timeSecToFillBotBar = 5;

        float topCurrentVisualProgressOfLerp = 0;
        float topDesiredBarValue = 0;
        float topPercentFilled = 0;
        public int timeSecToFillTopBar = 0;



        private void TimerUpdateProgressBarsGraphically(object sender, EventArgs e)
        {

           UpdateTopBar();
           UpdateBottomBar();




        }

        private void UpdateTopBar()
        {
            if (timeSecToFillTopBar == 0)
                return;
            if (ArtistActive)
            {

                float rest = (float)(activatedFullTime.TotalSeconds % (timeSecToFillTopBar));
                topPercentFilled = Utils.ToProcentage(rest, 0, timeSecToFillTopBar);
                topDesiredBarValue = (int)Utils.ProcentToProgressBarValue(progressBarTopMost, topPercentFilled);
                progressBarTopMost.Value = topDesiredBarValue;

            }




        }

        private void UpdateBottomBar()
        {
            if (ArtistActive)
            {

                float rest = (float)(activatedFullTime.TotalSeconds % (timeSecToFillBotBar));
                botPercentFilled = Utils.ToProcentage(rest, 0, timeSecToFillBotBar);
                botDesiredBarValue = Utils.ProcentToProgressBarValue(progressBarBottomMost, botPercentFilled);
                progressBarBottomMost.Value = botDesiredBarValue;

            }
        }

        //private void UpdateBottomBar()
        //{
        //    if (ArtistActive)
        //    {

        //        float rest = (float)(activatedFullTime.TotalSeconds % (timeSecToFillBotBar));
        //        botPercentFilled = Utils.ToProcentage(rest, 0, timeSecToFillBotBar);
        //        botDesiredBarValue = Utils.ProcentToProgressBarValue(progressBarBottomMost, botPercentFilled);
        //    }



        //    if (botCurrentVisualProgressOfLerp > botDesiredBarValue)
        //    {
        //        botCurrentVisualProgressOfLerp = botDesiredBarValue;
        //        progressBarBottomMost.Value = botDesiredBarValue;
        //    }

        //    float lerpSpeed = botPercentFilled / 100;
        //    botCurrentVisualProgressOfLerp = Utils.Lerp(botCurrentVisualProgressOfLerp, botDesiredBarValue, lerpSpeed);

        //    progressBarBottomMost.Value = botCurrentVisualProgressOfLerp < 0 ? 0 : botCurrentVisualProgressOfLerp;

        //    label1.Text = progressBarBottomMost.Value.ToString();
        //}

        MilestoneSystem milestoneSystem;

        private void CreateMilestoneSystem()
        {
            milestoneSystem = new MilestoneSystem(this);

        }

        NotificationSystem notificationSystem;
        private void CreateNotificationSystem()
        {
            notificationSystem = new NotificationSystem(this);

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

        private void SaveWindowPosition()
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

        private void FormMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {


            if (Control.MouseButtons == MouseButtons.None)
                isMouseDown = false;

            if (e.Button == MouseButtons.Left)
                isLMouseDown = false;

            if (e.Button == MouseButtons.Right)
            {
                isRMouseDown = false;

                SaveWindowPosition();


            }

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            trayIcon.Icon = null;
            trayIcon.Visible = false;
            trayIcon.Dispose();

            base.OnFormClosed(e);

        }



        public void SetMidLable(string text)
        {
            label3.Text = text;
        }

        public TimeSpan GetActivatedTime()
        {
            return activatedFullTime;
        }

        public PictureBox GetNotificationPictureBox()
        {
            return pictureBoxNotification;
        }
        public Label GetNotificationLabel()
        {
            return labelNotification;
        }

        public void SetDailyGoal(TimeSpan time)
        {
            label4.Text = "Daily goal:";
            label5.Text = time.ToString();



            timeSecToFillTopBar = (int)time.TotalSeconds;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //progressBarBottomMost.Value -= 20;

        }
        private void label3_Click(object sender, EventArgs e)
        {
            //notificationSystem.NotifyUser(new Notification(Milestone.GetDefaultMilestoneMessage(), false, TimeSpan.FromSeconds(3)));

           
            //    progressBarBottomMost.Value += 20;
           
        }

        private void label4_MouseClick(object sender, MouseEventArgs e)
        {
            DailyGoalForm dialog = new DailyGoalForm();
            var result = dialog.ShowDialog();
            if(result == DialogResult.OK)
            SetDailyGoal(dialog.returnTime);



        }

        private void flowLayoutPanel1Parent_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ShowNotification(Notification notification)
        {
            notificationSystem.ShowNotification(notification);
        }
    }
}
