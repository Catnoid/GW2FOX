namespace GW2FOX
{
    partial class LLA
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LLA));
            Symbols = new TextBox();
            Welcome = new TextBox();
            Guild = new TextBox();
            Squadinfo = new TextBox();
            Runinfo = new TextBox();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            pictureBox2 = new PictureBox();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            Leyline20 = new TextBox();
            Leyline50 = new TextBox();
            Leyline60 = new TextBox();
            button10 = new Button();
            button11 = new Button();
            button12 = new Button();
            Oofll20 = new TextBox();
            Oofll50 = new TextBox();
            Oofll60 = new TextBox();
            button13 = new Button();
            button14 = new Button();
            button15 = new Button();
            pictureBox3 = new PictureBox();
            pictureBox5 = new PictureBox();
            button16 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // Symbols
            // 
            Symbols.Font = new Font("Segoe UI", 15F);
            Symbols.Location = new Point(1458, 12);
            Symbols.Multiline = true;
            Symbols.Name = "Symbols";
            Symbols.ReadOnly = true;
            Symbols.ScrollBars = ScrollBars.Vertical;
            Symbols.Size = new Size(434, 221);
            Symbols.TabIndex = 18;
            // 
            // Welcome
            // 
            Welcome.Font = new Font("Segoe UI", 15F);
            Welcome.Location = new Point(1440, 268);
            Welcome.Multiline = true;
            Welcome.Name = "Welcome";
            Welcome.ReadOnly = true;
            Welcome.ScrollBars = ScrollBars.Vertical;
            Welcome.Size = new Size(450, 100);
            Welcome.TabIndex = 17;
            // 
            // Guild
            // 
            Guild.Font = new Font("Segoe UI", 15F);
            Guild.Location = new Point(965, 268);
            Guild.Multiline = true;
            Guild.Name = "Guild";
            Guild.ReadOnly = true;
            Guild.ScrollBars = ScrollBars.Vertical;
            Guild.Size = new Size(450, 100);
            Guild.TabIndex = 16;
            // 
            // Squadinfo
            // 
            Squadinfo.Font = new Font("Segoe UI", 15F);
            Squadinfo.Location = new Point(490, 268);
            Squadinfo.Multiline = true;
            Squadinfo.Name = "Squadinfo";
            Squadinfo.ReadOnly = true;
            Squadinfo.ScrollBars = ScrollBars.Vertical;
            Squadinfo.Size = new Size(450, 100);
            Squadinfo.TabIndex = 15;
            Squadinfo.Text = "\r\n";
            // 
            // Runinfo
            // 
            Runinfo.Font = new Font("Segoe UI", 15F);
            Runinfo.Location = new Point(15, 268);
            Runinfo.Multiline = true;
            Runinfo.Name = "Runinfo";
            Runinfo.ReadOnly = true;
            Runinfo.ScrollBars = ScrollBars.Vertical;
            Runinfo.Size = new Size(450, 100);
            Runinfo.TabIndex = 14;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Header;
            pictureBox1.Location = new Point(602, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(700, 175);
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.BackgroundImage = Properties.Resources.ButtonBack;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(1793, 900);
            button1.Name = "button1";
            button1.Size = new Size(100, 102);
            button1.TabIndex = 24;
            button1.UseVisualStyleBackColor = false;
            button1.Click += Back_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.WBsheader2;
            pictureBox2.Location = new Point(14, 403);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(1877, 30);
            pictureBox2.TabIndex = 28;
            pictureBox2.TabStop = false;
            // 
            // button2
            // 
            button2.Location = new Point(15, 374);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 29;
            button2.Text = "Copy";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Runinfoload_Click;
            // 
            // button3
            // 
            button3.Location = new Point(490, 374);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 30;
            button3.Text = "Copy";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Squadinfoload_Click;
            // 
            // button4
            // 
            button4.Location = new Point(965, 374);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 31;
            button4.Text = "Copy";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Guild_Click;
            // 
            // button5
            // 
            button5.Location = new Point(1440, 374);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 32;
            button5.Text = "Copy";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Welcome_Click;
            // 
            // Leyline20
            // 
            Leyline20.BackColor = SystemColors.Highlight;
            Leyline20.Font = new Font("Segoe UI", 15F);
            Leyline20.Location = new Point(15, 439);
            Leyline20.Multiline = true;
            Leyline20.Name = "Leyline20";
            Leyline20.ReadOnly = true;
            Leyline20.ScrollBars = ScrollBars.Vertical;
            Leyline20.Size = new Size(600, 60);
            Leyline20.TabIndex = 37;
            Leyline20.Text = "☠LvL 20 ≪Legend Ley Line Anomaly≫ │ ☣[&BOQAAAA=]☣";
            // 
            // Leyline50
            // 
            Leyline50.BackColor = SystemColors.Highlight;
            Leyline50.Font = new Font("Segoe UI", 15F);
            Leyline50.Location = new Point(14, 534);
            Leyline50.Multiline = true;
            Leyline50.Name = "Leyline50";
            Leyline50.ReadOnly = true;
            Leyline50.ScrollBars = ScrollBars.Vertical;
            Leyline50.Size = new Size(600, 60);
            Leyline50.TabIndex = 38;
            Leyline50.Text = "☠LvL 50 ≪Legend Ley Line Anomaly≫ │ ☣[&BEwCAAA=]☣\r\n";
            // 
            // Leyline60
            // 
            Leyline60.BackColor = SystemColors.Highlight;
            Leyline60.Font = new Font("Segoe UI", 15F);
            Leyline60.Location = new Point(14, 629);
            Leyline60.Multiline = true;
            Leyline60.Name = "Leyline60";
            Leyline60.ReadOnly = true;
            Leyline60.ScrollBars = ScrollBars.Vertical;
            Leyline60.Size = new Size(600, 60);
            Leyline60.TabIndex = 39;
            Leyline60.Text = "☠Lvl 60 ≪Legend Ley-Line-Anomaly≫ │ ☣[&BOcBAAA=]☣";
            // 
            // button10
            // 
            button10.Location = new Point(14, 505);
            button10.Name = "button10";
            button10.Size = new Size(75, 23);
            button10.TabIndex = 40;
            button10.Text = "Copy";
            button10.UseVisualStyleBackColor = true;
            button10.Click += Ll20_Click;
            // 
            // button11
            // 
            button11.Location = new Point(14, 600);
            button11.Name = "button11";
            button11.Size = new Size(75, 23);
            button11.TabIndex = 41;
            button11.Text = "Copy";
            button11.UseVisualStyleBackColor = true;
            button11.Click += Ll50_Click;
            // 
            // button12
            // 
            button12.Location = new Point(14, 695);
            button12.Name = "button12";
            button12.Size = new Size(75, 23);
            button12.TabIndex = 42;
            button12.Text = "Copy";
            button12.UseVisualStyleBackColor = true;
            button12.Click += Ll60_Click;
            // 
            // Oofll20
            // 
            Oofll20.Font = new Font("Segoe UI", 15F);
            Oofll20.Location = new Point(655, 439);
            Oofll20.Multiline = true;
            Oofll20.Name = "Oofll20";
            Oofll20.ReadOnly = true;
            Oofll20.ScrollBars = ScrollBars.Vertical;
            Oofll20.Size = new Size(600, 60);
            Oofll20.TabIndex = 43;
            Oofll20.Text = "Go out of fight, then right click group 2 & join \r\n ☣Gendarran Fields☣";
            // 
            // Oofll50
            // 
            Oofll50.Font = new Font("Segoe UI", 15F);
            Oofll50.Location = new Point(655, 534);
            Oofll50.Multiline = true;
            Oofll50.Name = "Oofll50";
            Oofll50.ReadOnly = true;
            Oofll50.ScrollBars = ScrollBars.Vertical;
            Oofll50.Size = new Size(600, 60);
            Oofll50.TabIndex = 44;
            Oofll50.Text = "Go out of fight, then right click group 2 & join \r\n ☣Timberline Falls☣";
            // 
            // Oofll60
            // 
            Oofll60.Font = new Font("Segoe UI", 15F);
            Oofll60.Location = new Point(655, 629);
            Oofll60.Multiline = true;
            Oofll60.Name = "Oofll60";
            Oofll60.ReadOnly = true;
            Oofll60.ScrollBars = ScrollBars.Vertical;
            Oofll60.Size = new Size(600, 60);
            Oofll60.TabIndex = 45;
            Oofll60.Text = "Go out of fight, then right click group 2 & join \r\n ☣Iron Marches☣";
            // 
            // button13
            // 
            button13.Location = new Point(655, 505);
            button13.Name = "button13";
            button13.Size = new Size(75, 23);
            button13.TabIndex = 46;
            button13.Text = "Copy";
            button13.UseVisualStyleBackColor = true;
            button13.Click += Instancell20_Click;
            // 
            // button14
            // 
            button14.Location = new Point(655, 600);
            button14.Name = "button14";
            button14.Size = new Size(75, 23);
            button14.TabIndex = 47;
            button14.Text = "Copy";
            button14.UseVisualStyleBackColor = true;
            button14.Click += Instancell50_Click;
            // 
            // button15
            // 
            button15.Location = new Point(655, 695);
            button15.Name = "button15";
            button15.Size = new Size(75, 23);
            button15.TabIndex = 48;
            button15.Text = "Copy";
            button15.UseVisualStyleBackColor = true;
            button15.Click += Instancell60_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.Line;
            pictureBox3.Location = new Point(14, 724);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(1880, 30);
            pictureBox3.TabIndex = 80;
            pictureBox3.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.WBsheader;
            pictureBox5.Location = new Point(12, 232);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(1880, 30);
            pictureBox5.TabIndex = 126;
            pictureBox5.TabStop = false;
            // 
            // button16
            // 
            button16.BackgroundImage = Properties.Resources.wikigw2;
            button16.FlatStyle = FlatStyle.Flat;
            button16.Location = new Point(10, 11);
            button16.Name = "button16";
            button16.Size = new Size(100, 45);
            button16.TabIndex = 127;
            button16.UseVisualStyleBackColor = true;
            button16.Click += button16_Click;
            // 
            // LLA
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            ClientSize = new Size(1904, 1041);
            Controls.Add(button16);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox3);
            Controls.Add(button15);
            Controls.Add(button14);
            Controls.Add(button13);
            Controls.Add(Oofll60);
            Controls.Add(Oofll50);
            Controls.Add(Oofll20);
            Controls.Add(button12);
            Controls.Add(button11);
            Controls.Add(button10);
            Controls.Add(Leyline60);
            Controls.Add(Leyline50);
            Controls.Add(Leyline20);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(pictureBox2);
            Controls.Add(button1);
            Controls.Add(Symbols);
            Controls.Add(Welcome);
            Controls.Add(Guild);
            Controls.Add(Squadinfo);
            Controls.Add(Runinfo);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LLA";
            StartPosition = FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox Symbols;
        private TextBox Welcome;
        private TextBox Guild;
        private TextBox Squadinfo;
        private TextBox Runinfo;
        private PictureBox pictureBox1;
        private Button button1;
        private PictureBox pictureBox2;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private TextBox Leyline20;
        private TextBox Leyline50;
        private TextBox Leyline60;
        private Button button10;
        private Button button11;
        private Button button12;
        private TextBox Oofll20;
        private TextBox Oofll50;
        private TextBox Oofll60;
        private Button button13;
        private Button button14;
        private Button button15;
        private PictureBox pictureBox3;
        private PictureBox pictureBox5;
        private Button button16;
    }
}