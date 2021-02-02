using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace DFA
{
    public class ProgressBarSoft : Control
    {

        public ProgressBarSoft()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            Maximum = 100;
            this.ForeColor = Color.Blue;
            this.BackColor = Color.Maroon;
            this.DoubleBuffered = true;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [Description("Use lerp to smoothly transition between values over time")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]

        private bool mWithLerp;
        public bool WithLerp
        {
            get
            {
                return mWithLerp;
            }
            set
            {

                //if (value)
                //{
                //    if (!IsLerping)
                //        InitLerp();
                //}
                //else
                //{
                //    if (IsLerping)
                //        StopLerp();
                //}
                mWithLerp = value;

            }
        }


        public float Minimum { get; set; }
        public float Maximum { get; set; }

        private const int UpdateIntervalInMiliseconds = 1;
        public bool IsLerping => LerpRunning != null;
        private Timer LerpRunning;
        private Timer lerpTimer;

        private float lerpTargetValue;


        private float mValue;
        public float Value
        {
            get { return mValue; }
            set
            {
                if (value < Minimum)
                    value = Minimum;

                if (value > Maximum)
                    value = Maximum;

                if (!WithLerp)
                {
                    mValue = value;
                    Invalidate();

                }
                else
                {
                    if (!IsLerping)
                        InitLerp();

                    lerpTargetValue = value;




                    //  if (ArtistActive)
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
                }

            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            var fillRect = new RectangleF(0, 0, (float)(this.Width * (  Value - Minimum) / Maximum), this.Height);


            using (var foreBrush = new SolidBrush(this.ForeColor))
            {
                e.Graphics.FillRectangle(foreBrush, fillRect);
            }



            base.OnPaint(e);
        }

        private void StopLerp()
        {
            if(IsLerping)
            {
                lerpTimer.Stop();
                lerpTimer.Dispose();
            }
        }

        private void InitLerp()
        {
            StopLerp();
            lerpTimer = new Timer();
            lerpTimer.Interval = UpdateIntervalInMiliseconds;
            lerpTimer.Tick += new EventHandler(LerpTick);
            lerpTimer.Start();

        }

        private void LerpTick(object sender, EventArgs e)
        {

            mValue = Utils.Lerp(mValue, lerpTargetValue, 0.01f);
            Invalidate();

        }

    }
}
