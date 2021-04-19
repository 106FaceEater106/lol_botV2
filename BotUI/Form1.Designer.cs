
namespace BotUI {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.botVer = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.mode_lable = new System.Windows.Forms.Label();
            this.boopButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // botVer
            // 
            this.botVer.AutoSize = true;
            this.botVer.Location = new System.Drawing.Point(12, 49);
            this.botVer.Name = "botVer";
            this.botVer.Size = new System.Drawing.Size(38, 15);
            this.botVer.TabIndex = 0;
            this.botVer.Text = "label1";
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(12, 120);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.AutoEllipsis = true;
            this.stopButton.Location = new System.Drawing.Point(12, 149);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // mode_lable
            // 
            this.mode_lable.AutoSize = true;
            this.mode_lable.Location = new System.Drawing.Point(12, 64);
            this.mode_lable.Name = "mode_lable";
            this.mode_lable.Size = new System.Drawing.Size(38, 15);
            this.mode_lable.TabIndex = 4;
            this.mode_lable.Text = "label1";
            // 
            // boopButton
            // 
            this.boopButton.Location = new System.Drawing.Point(154, 0);
            this.boopButton.Name = "boopButton";
            this.boopButton.Size = new System.Drawing.Size(75, 23);
            this.boopButton.TabIndex = 6;
            this.boopButton.Text = "Boop";
            this.boopButton.UseVisualStyleBackColor = true;
            this.boopButton.Click += new System.EventHandler(this.boop_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 186);
            this.Controls.Add(this.boopButton);
            this.Controls.Add(this.mode_lable);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.botVer);
            this.Name = "Form1";
            this.Text = "a";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label botVer;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label mode_lable;
        private System.Windows.Forms.Button boopButton;
        private System.Windows.Forms.Timer timer1;
    }
}

