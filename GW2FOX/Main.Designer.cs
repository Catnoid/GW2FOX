namespace GW2FOX
{
    partial class Main
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

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            pictureBox1 = new PictureBox();
            button4 = new Button();
            button3 = new Button();
            button5 = new Button();
            button1 = new Button();
            button2 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Header;
            pictureBox1.Location = new Point(42, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(700, 175);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaptionText;
            button4.BackgroundImage = Properties.Resources.FOXHomepage;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Location = new Point(67, 193);
            button4.Name = "button4";
            button4.Size = new Size(182, 47);
            button4.TabIndex = 5;
            button4.UseVisualStyleBackColor = false;
            button4.Click += Fox_Click;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ActiveCaptionText;
            button3.BackgroundImage = Properties.Resources.Repair;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(67, 246);
            button3.Name = "button3";
            button3.Size = new Size(182, 47);
            button3.TabIndex = 8;
            button3.UseVisualStyleBackColor = false;
            button3.Click += Repair_Click;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ActiveCaptionText;
            button5.BackgroundImage = Properties.Resources.OTimer;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(67, 352);
            button5.Name = "button5";
            button5.Size = new Size(182, 47);
            button5.TabIndex = 10;
            button5.UseVisualStyleBackColor = false;
            button5.Click += Timer_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaptionText;
            button1.BackgroundImage = Properties.Resources.LeadingHelper;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(67, 299);
            button1.Name = "button1";
            button1.Size = new Size(182, 47);
            button1.TabIndex = 11;
            button1.UseVisualStyleBackColor = false;
            button1.Click += Leading_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaptionText;
            button2.BackgroundImage = Properties.Resources.Close;
            button2.Location = new Point(669, 375);
            button2.Name = "button2";
            button2.Size = new Size(100, 20);
            button2.TabIndex = 12;
            button2.UseVisualStyleBackColor = false;
            button2.Click += CloseAll_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.Black;
            button6.BackgroundImage = Properties.Resources.logo;
            button6.Location = new Point(427, 208);
            button6.Name = "button6";
            button6.Size = new Size(140, 140);
            button6.TabIndex = 14;
            button6.UseVisualStyleBackColor = false;
            button6.Click += BlishHUD_Click;
            // 
            // button7
            // 
            button7.BackColor = Color.Black;
            button7.BackgroundImage = Properties.Resources.Screenshot_2024_03_28_162930;
            button7.Location = new Point(573, 208);
            button7.Name = "button7";
            button7.Size = new Size(140, 140);
            button7.TabIndex = 15;
            button7.UseVisualStyleBackColor = false;
            button7.Click += ReShade_Click;
            // 
            // button8
            // 
            button8.BackColor = Color.Black;
            button8.BackgroundImage = Properties.Resources.Taca;
            button8.Location = new Point(281, 208);
            button8.Name = "button8";
            button8.Size = new Size(140, 140);
            button8.TabIndex = 16;
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = Properties.Resources.Shortcut;
            pictureBox2.Location = new Point(255, 365);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(160, 25);
            pictureBox2.TabIndex = 17;
            pictureBox2.TabStop = false;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Backgroundmini;
            ClientSize = new Size(784, 411);
            Controls.Add(pictureBox2);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button4;
        private Button button3;
        private Button button5;
        private Button button1;
        private Button button2;
        private Button button6;
        private Button button7;
        private Button button8;
        private PictureBox pictureBox2;
    }
}
