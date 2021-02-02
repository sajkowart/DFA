using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DFA.Forms
{
    public partial class DailyGoalForm
    {

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code




        private void InitializeComponent()
        {
            this.buttonAccept = new System.Windows.Forms.Button();
            this.textBoxInputTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAccept
            // 
            this.buttonAccept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAccept.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.buttonAccept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(118)))), ((int)(((byte)(135)))));
            this.buttonAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAccept.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonAccept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.buttonAccept.Location = new System.Drawing.Point(110, 127);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 46);
            this.buttonAccept.TabIndex = 0;
            this.buttonAccept.Text = "Accept";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonAccept_MouseClick);
            // 
            // textBoxInputTime
            // 
            this.textBoxInputTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.textBoxInputTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxInputTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.textBoxInputTime.Location = new System.Drawing.Point(58, 84);
            this.textBoxInputTime.Name = "textBoxInputTime";
            this.textBoxInputTime.Size = new System.Drawing.Size(183, 23);
            this.textBoxInputTime.TabIndex = 1;
            this.textBoxInputTime.TextChanged += new System.EventHandler(this.TextBoxInputTime_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label1.Location = new System.Drawing.Point(58, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 72);
            this.label1.TabIndex = 2;
            this.label1.Text = "Input daily goal time (HH:MM ex. 8:00)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DailyGoalForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(300, 185);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxInputTime);
            this.Controls.Add(this.buttonAccept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DailyGoalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private Label label1;
        private TextBox textBoxInputTime;
        private Button buttonAccept;
    }


}
