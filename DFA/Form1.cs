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

    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern bool RegisterRawInputDevices(RAWINPUTDEVICE pRawInputDevices, uint uiNumDevices, uint cbSize);

        public IntPtr HWnd { get; set; }

        public string Label1
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }
        public string Label2
        {
            get
            {
                return this.label2.Text;
            }
            set
            {
                this.label2.Text = value;
            }
        }
        public string Label3
        {
            get
            {
                return this.label3.Text;
            }
            set
            {
                this.label3.Text = value;
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public uint dwFlags;
            public IntPtr hwndTarget;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
           // e.Graphics.FillRectangle(Brushes.Black, e.ClipRectangle);


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit; // This makes the diffrence otherwise it does look exactly the same!
            g.DrawString(msg, new Font("Tahoma", 15), Brushes.Black, 0, 0);

            base.OnPaint(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            label4.Parent = pictureBox1;
            label4.BackColor = Color.Transparent;


            

        }
        public Form1()
        {
            
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.BackColor = Color.DarkGray;
            this.TransparencyKey = Color.DarkGray;




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
                Label1 = "registration failed";
            }
            else
            {
                Label1 = "registration success";

            }

            Timer timer = new Timer();
            timer.Interval = (1 * 1000); // 10 secs
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();


        }

        
        private void timer_Tick(object sender, EventArgs e)
        {
            Label2 =  DateTime.Now.Second.ToString() + ": " + msg;

            Label3 = msgFromInput;
            this.InvokePaint(this, new PaintEventArgs(this.CreateGraphics(), this.DisplayRectangle));

        }

        String msg;
        String msgFromInput;

        public enum MsgType{
            WM_INPUT =0x00FF

        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            //Console.WriteLine(m);

            msg = m.Msg.ToString();

            switch (m.Msg)
            {
                case (int) MsgType.WM_INPUT:

                    msgFromInput = m.LParam.ToString();
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


            base.WndProc(ref m);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
