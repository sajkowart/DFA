using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DFA
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.pictureBoxParent = new System.Windows.Forms.PictureBox();
            this.mainProgressBar = new DFA.NewProgressBar();
            this.topMostProgressBar = new DFA.NewProgressBar();
            this.flowLayoutPanelParent = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutMiddle = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2TimeLabelText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxParent)).BeginInit();
            this.flowLayoutPanelParent.SuspendLayout();
            this.flowLayoutMiddle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxParent
            // 
            this.pictureBoxParent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pictureBoxParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxParent.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxParent.Image")));
            this.pictureBoxParent.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxParent.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxParent.Name = "pictureBoxParent";
            this.pictureBoxParent.Size = new System.Drawing.Size(1000, 40);
            this.pictureBoxParent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxParent.TabIndex = 0;
            this.pictureBoxParent.TabStop = false;
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.mainProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(147)))), ((int)(((byte)(216)))));
            this.mainProgressBar.Location = new System.Drawing.Point(0, 27);
            this.mainProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this.mainProgressBar.Maximum = 10000;
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(1000, 8);
            this.mainProgressBar.Step = 1;
            this.mainProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.mainProgressBar.TabIndex = 5;
            this.mainProgressBar.Value = 1010;
            // 
            // topMostProgressBar
            // 
            this.topMostProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.topMostProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topMostProgressBar.ForeColor = System.Drawing.Color.PaleGreen;
            this.topMostProgressBar.Location = new System.Drawing.Point(0, 0);
            this.topMostProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this.topMostProgressBar.Maximum = 10000;
            this.topMostProgressBar.MaximumSize = new System.Drawing.Size(1000, 2);
            this.topMostProgressBar.MinimumSize = new System.Drawing.Size(1000, 2);
            this.topMostProgressBar.Name = "topMostProgressBar";
            this.topMostProgressBar.Size = new System.Drawing.Size(1000, 2);
            this.topMostProgressBar.Step = 1;
            this.topMostProgressBar.TabIndex = 6;
            this.topMostProgressBar.Value = 5000;
            // 
            // flowLayoutPanelParent
            // 
            this.flowLayoutPanelParent.AutoSize = true;
            this.flowLayoutPanelParent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelParent.Controls.Add(this.topMostProgressBar);
            this.flowLayoutPanelParent.Controls.Add(this.flowLayoutMiddle);
            this.flowLayoutPanelParent.Controls.Add(this.mainProgressBar);
            this.flowLayoutPanelParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelParent.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelParent.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelParent.MinimumSize = new System.Drawing.Size(1000, 20);
            this.flowLayoutPanelParent.Name = "flowLayoutPanelParent";
            this.flowLayoutPanelParent.Size = new System.Drawing.Size(1000, 40);
            this.flowLayoutPanelParent.TabIndex = 7;
            // 
            // flowLayoutMiddle
            // 
            this.flowLayoutMiddle.AutoSize = true;
            this.flowLayoutMiddle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutMiddle.Controls.Add(this.label1);
            this.flowLayoutMiddle.Controls.Add(this.label2TimeLabelText);
            this.flowLayoutMiddle.Controls.Add(this.label3);
            this.flowLayoutMiddle.Controls.Add(this.label4);
            this.flowLayoutMiddle.Controls.Add(this.label5);
            this.flowLayoutMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutMiddle.Location = new System.Drawing.Point(0, 2);
            this.flowLayoutMiddle.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutMiddle.MinimumSize = new System.Drawing.Size(1000, 15);
            this.flowLayoutMiddle.Name = "flowLayoutMiddle";
            this.flowLayoutMiddle.Size = new System.Drawing.Size(1000, 25);
            this.flowLayoutMiddle.TabIndex = 8;
            this.flowLayoutMiddle.WrapContents = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(147)))), ((int)(((byte)(216)))));
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label1.MaximumSize = new System.Drawing.Size(200, 20);
            this.label1.MinimumSize = new System.Drawing.Size(100, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2TimeLabelText
            // 
            this.label2TimeLabelText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label2TimeLabelText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(147)))), ((int)(((byte)(216)))));
            this.label2TimeLabelText.Location = new System.Drawing.Point(200, 5);
            this.label2TimeLabelText.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label2TimeLabelText.MaximumSize = new System.Drawing.Size(200, 20);
            this.label2TimeLabelText.MinimumSize = new System.Drawing.Size(100, 15);
            this.label2TimeLabelText.Name = "label2TimeLabelText";
            this.label2TimeLabelText.Size = new System.Drawing.Size(200, 15);
            this.label2TimeLabelText.TabIndex = 2;
            this.label2TimeLabelText.Text = "TimeLabelText";
            this.label2TimeLabelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(147)))), ((int)(((byte)(216)))));
            this.label3.Location = new System.Drawing.Point(400, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label3.MaximumSize = new System.Drawing.Size(200, 20);
            this.label3.MinimumSize = new System.Drawing.Size(100, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "label3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(147)))), ((int)(((byte)(216)))));
            this.label4.Location = new System.Drawing.Point(600, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label4.MaximumSize = new System.Drawing.Size(200, 20);
            this.label4.MinimumSize = new System.Drawing.Size(100, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.UseCompatibleTextRendering = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(147)))), ((int)(((byte)(216)))));
            this.label5.Location = new System.Drawing.Point(800, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label5.MaximumSize = new System.Drawing.Size(200, 20);
            this.label5.MinimumSize = new System.Drawing.Size(100, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1000, 40);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanelParent);
            this.Controls.Add(this.pictureBoxParent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 100);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 40);
            this.Name = "Form2";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxParent)).EndInit();
            this.flowLayoutPanelParent.ResumeLayout(false);
            this.flowLayoutPanelParent.PerformLayout();
            this.flowLayoutMiddle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private PictureBox pictureBoxParent;
        private NewProgressBar mainProgressBar;
        private NewProgressBar topMostProgressBar;
        private FlowLayoutPanel flowLayoutPanelParent;

        private Label label1;
        private Label label2TimeLabelText;
        private Label label3;
        private Label label4;
        private Label label5;
        private FlowLayoutPanel flowLayoutMiddle;
    }
}

