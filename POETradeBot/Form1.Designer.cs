using System;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace POETradeBot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.waitForInvTimer = new System.Timers.Timer();
            this.screenshot = new System.Windows.Forms.PictureBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.patternLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.waitForTrade = new System.Timers.Timer();
            this.pattern = new System.Windows.Forms.PictureBox();
            this.textboxId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.waitForInvTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenshot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitForTrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pattern)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(238, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(127, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Search Item";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 163);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(158, 134);
            this.listBox1.TabIndex = 4;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "No Items Found.";
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(176, 53);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(140, 21);
            this.comboBox2.TabIndex = 6;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(304, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "label3";
            // 
            // waitForInvTimer
            // 
            this.waitForInvTimer.Interval = 2000D;
            this.waitForInvTimer.SynchronizingObject = this;
            this.waitForInvTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.WaitForPartyInvite);
            // 
            // screenshot
            // 
            this.screenshot.Location = new System.Drawing.Point(176, 190);
            this.screenshot.Name = "screenshot";
            this.screenshot.Size = new System.Drawing.Size(140, 48);
            this.screenshot.TabIndex = 10;
            this.screenshot.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(181, 18);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(135, 20);
            this.textBox2.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(238, 108);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "by URL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // patternLabel
            // 
            this.patternLabel.Location = new System.Drawing.Point(176, 241);
            this.patternLabel.Name = "patternLabel";
            this.patternLabel.Size = new System.Drawing.Size(72, 16);
            this.patternLabel.TabIndex = 13;
            this.patternLabel.Text = "pattern";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(176, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 14;
            this.label5.Text = "screen";
            // 
            // waitForTrade
            // 
            this.waitForTrade.Interval = 2000D;
            this.waitForTrade.SynchronizingObject = this;
            this.waitForTrade.Elapsed += new System.Timers.ElapsedEventHandler(this.waitForTrade_Elapsed);
            // 
            // pattern
            // 
            this.pattern.Location = new System.Drawing.Point(176, 260);
            this.pattern.Name = "pattern";
            this.pattern.Size = new System.Drawing.Size(140, 37);
            this.pattern.TabIndex = 15;
            this.pattern.TabStop = false;
            // 
            // textboxId
            // 
            this.textboxId.Location = new System.Drawing.Point(9, 110);
            this.textboxId.Name = "textboxId";
            this.textboxId.Size = new System.Drawing.Size(223, 20);
            this.textboxId.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(9, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "paste Trade URL";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(325, 322);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textboxId);
            this.Controls.Add(this.pattern);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.patternLabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.screenshot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.waitForInvTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.screenshot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.waitForTrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pattern)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox textboxId;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.PictureBox pattern;

        private System.Windows.Forms.Label patternLabel;

        private Timer waitForTrade;

        private System.Windows.Forms.Label label5;
        
        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.TextBox textBox2;
        
        private System.Windows.Forms.PictureBox screenshot;

        private Timer waitForInvTimer;

        private System.Windows.Forms.Label label3;

        private ComboBox comboBox2;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.ListBox listBox1;

        private Label label1;

        private ComboBox comboBox1;

        private System.Windows.Forms.Button button1;
        private TextBox textBox1;

        #endregion
    }
}