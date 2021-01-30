using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace DFA
{
    public class NewProgressBar : ProgressBar
    {
        public NewProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // None... Helps control the flicker.
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            const int inset = 0; // A single inset value to control teh sizing of the inner rect.

            using (Image offscreenImage = new Bitmap(this.Width, this.Height))
            {
                using (Graphics offscreen = Graphics.FromImage(offscreenImage))
                {




                    Rectangle fillRect = new Rectangle(0, 0, this.Width, this.Height);
                    offscreen.FillRectangle(new SolidBrush(BackColor), fillRect);


                    // if (ProgressBarRenderer.IsSupported)
                    //  ProgressBarRenderer.DrawHorizontalBar(offscreen, rect);



                    //    rect.Inflate(new Size(-inset, -inset)); // Deflate inner rect.
                    fillRect.Width = (int)(fillRect.Width * ((double)this.Value / this.Maximum));
                    if (fillRect.Width == 0) fillRect.Width = 1;
                    // Can't draw rec with width of 0.
                    //20; 20; 20    187; 143; 206   newprpl : 206; 147; 216
                    //def 
                    //LinearGradientBrush brush = new LinearGradientBrush(rect, this.BackColor, this.ForeColor, LinearGradientMode.Vertical);

                    //modified
                    //LinearGradientBrush brush = new LinearGradientBrush(fillRect, Color.FromArgb(220, 143, 250), this.ForeColor, LinearGradientMode.Vertical);

                    //solid
                    SolidBrush brush = new SolidBrush(ForeColor);

                    offscreen.FillRectangle(brush, inset, inset, fillRect.Width, fillRect.Height);

                    e.Graphics.DrawImage(offscreenImage, 0, 0);
                }
            }
        }
    }
}
