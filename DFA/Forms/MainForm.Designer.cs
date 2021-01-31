using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DFA
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2TimeLabelText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBarBottomMost = new DFA.NewProgressBar();
            this.progressBarTopMost = new DFA.NewProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1Parent = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1Parent.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMouseUp);
            // 
            // label2TimeLabelText
            // 
            this.label2TimeLabelText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label2TimeLabelText.CausesValidation = false;
            this.label2TimeLabelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2TimeLabelText.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2TimeLabelText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label2TimeLabelText.Location = new System.Drawing.Point(203, 0);
            this.label2TimeLabelText.Name = "label2TimeLabelText";
            this.label2TimeLabelText.Size = new System.Drawing.Size(194, 14);
            this.label2TimeLabelText.TabIndex = 2;
            this.label2TimeLabelText.Text = "timeLabelText";
            this.label2TimeLabelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2TimeLabelText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.label2TimeLabelText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            this.label2TimeLabelText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMouseUp);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label3.CausesValidation = false;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label3.Location = new System.Drawing.Point(403, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "label3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.label3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.label3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMouseUp);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label4.CausesValidation = false;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label4.Location = new System.Drawing.Point(603, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Click += new System.EventHandler(this.label5_Click);
            this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.label4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.label4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMouseUp);
            // 
            // progressBarBottomMost
            // 
            this.progressBarBottomMost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.progressBarBottomMost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarBottomMost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.progressBarBottomMost.Location = new System.Drawing.Point(0, 20);
            this.progressBarBottomMost.Margin = new System.Windows.Forms.Padding(0);
            this.progressBarBottomMost.Maximum = 10000;
            this.progressBarBottomMost.MaximumSize = new System.Drawing.Size(1500, 8);
            this.progressBarBottomMost.MinimumSize = new System.Drawing.Size(1000, 8);
            this.progressBarBottomMost.Name = "progressBarBottomMost";
            this.progressBarBottomMost.Size = new System.Drawing.Size(1000, 8);
            this.progressBarBottomMost.Step = 1;
            this.progressBarBottomMost.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarBottomMost.TabIndex = 5;
            this.progressBarBottomMost.Value = 1010;
            // 
            // progressBarTopMost
            // 
            this.progressBarTopMost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.progressBarTopMost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBarTopMost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.progressBarTopMost.Location = new System.Drawing.Point(0, 0);
            this.progressBarTopMost.Margin = new System.Windows.Forms.Padding(0);
            this.progressBarTopMost.MaximumSize = new System.Drawing.Size(1500, 2);
            this.progressBarTopMost.MinimumSize = new System.Drawing.Size(1000, 2);
            this.progressBarTopMost.Name = "progressBarTopMost";
            this.progressBarTopMost.Size = new System.Drawing.Size(1000, 2);
            this.progressBarTopMost.Step = 1;
            this.progressBarTopMost.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarTopMost.TabIndex = 6;
            this.progressBarTopMost.Value = 50;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2TimeLabelText, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(1500, 20);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(1000, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 14);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.label5.CausesValidation = false;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.label5.Location = new System.Drawing.Point(803, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "label5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            this.label5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.label5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            this.label5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMouseUp);
            // 
            // flowLayoutPanel1Parent
            // 
            this.flowLayoutPanel1Parent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1Parent.Controls.Add(this.progressBarTopMost);
            this.flowLayoutPanel1Parent.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel1Parent.Controls.Add(this.progressBarBottomMost);
            this.flowLayoutPanel1Parent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1Parent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1Parent.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1Parent.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1Parent.MaximumSize = new System.Drawing.Size(1500, 50);
            this.flowLayoutPanel1Parent.MinimumSize = new System.Drawing.Size(1000, 20);
            this.flowLayoutPanel1Parent.Name = "flowLayoutPanel1Parent";
            this.flowLayoutPanel1Parent.Size = new System.Drawing.Size(1000, 40);
            this.flowLayoutPanel1Parent.TabIndex = 8;
            this.flowLayoutPanel1Parent.WrapContents = false;
            this.flowLayoutPanel1Parent.MouseHover += new System.EventHandler(this.FormMouseHover);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1000, 40);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1Parent);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 100);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 28);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.FormDrawOver);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1Parent.ResumeLayout(false);
            this.flowLayoutPanel1Parent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Label label1;
        private Label label2TimeLabelText;
        private Label label3;
        private Label label4;
        private NewProgressBar progressBarBottomMost;
        private NewProgressBar progressBarTopMost;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label5;
        private FlowLayoutPanel flowLayoutPanel1Parent;
    }
}

