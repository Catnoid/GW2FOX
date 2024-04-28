namespace GW2FOX
{
    partial class PSNA
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
            txtAgentLocations = new TextBox();
            SuspendLayout();
            // 
            // txtAgentLocations
            // 
            txtAgentLocations.Location = new Point(12, 12);
            txtAgentLocations.Multiline = true;
            txtAgentLocations.Name = "txtAgentLocations";
            txtAgentLocations.ScrollBars = ScrollBars.Both;
            txtAgentLocations.Size = new Size(1315, 47);
            txtAgentLocations.TabIndex = 0;
            // 
            // PSNA
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1339, 69);
            Controls.Add(txtAgentLocations);
            Name = "PSNA";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtAgentLocations;
    }
}