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
            button4 = new Button();
            button3 = new Button();
            button5 = new Button();
            button2 = new Button();
            pictureBox2 = new PictureBox();
            button9 = new Button();
            button10 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.BackgroundImage = Properties.Resources.FOXHomepage;
            button4.Cursor = Cursors.Cross;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Location = new Point(12, 12);
            button4.Name = "button4";
            button4.Size = new Size(180, 50);
            button4.TabIndex = 5;
            button4.UseVisualStyleBackColor = false;
            button4.Click += Fox_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.BackgroundImage = Properties.Resources.Repair;
            button3.Cursor = Cursors.Cross;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(12, 68);
            button3.Name = "button3";
            button3.Size = new Size(180, 50);
            button3.TabIndex = 8;
            button3.UseVisualStyleBackColor = false;
            button3.Click += Repair_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Transparent;
            button5.BackgroundImage = Properties.Resources.OTimer;
            button5.Cursor = Cursors.Cross;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(12, 124);
            button5.Name = "button5";
            button5.Size = new Size(180, 50);
            button5.TabIndex = 10;
            button5.UseVisualStyleBackColor = false;
            button5.Click += Timer_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.BackgroundImage = Properties.Resources.Close;
            button2.FlatAppearance.BorderColor = Color.Black;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = SystemColors.WindowText;
            button2.Image = Properties.Resources.Close;
            button2.Location = new Point(62, 343);
            button2.Name = "button2";
            button2.Size = new Size(100, 100);
            button2.TabIndex = 12;
            button2.UseVisualStyleBackColor = false;
            button2.Click += CloseAll_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = Properties.Resources.Shortcut;
            pictureBox2.Location = new Point(77, 180);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(63, 25);
            pictureBox2.TabIndex = 17;
            pictureBox2.TabStop = false;
            // 
            // button9
            // 
            button9.BackgroundImage = Properties.Resources.ArcDPSinstall;
            button9.Location = new Point(40, 211);
            button9.Name = "button9";
            button9.Size = new Size(140, 60);
            button9.TabIndex = 18;
            button9.UseVisualStyleBackColor = true;
            button9.Click += ArcDPSInstall_Click;
            // 
            // button10
            // 
            button10.BackgroundImage = Properties.Resources.ArcDPSDeinstall;
            button10.Location = new Point(40, 277);
            button10.Name = "button10";
            button10.Size = new Size(140, 60);
            button10.TabIndex = 19;
            button10.UseVisualStyleBackColor = true;
            button10.Click += ArcDPSDeinstall_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.Backgroundmini;
            ClientSize = new Size(212, 450);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(pictureBox2);
            Controls.Add(button2);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(button4);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button button4;
        private Button button3;
        private Button button5;
        private Button button2;
        private PictureBox pictureBox2;
        private Button button9;
        private Button button10;
    }
}
