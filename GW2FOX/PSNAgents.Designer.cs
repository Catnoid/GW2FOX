namespace GW2FOX
{
    partial class PSNAgents
    {
        private System.Windows.Forms.Button btnGetChatLinks;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChatLink;

        private void InitializeComponent()
        {
            this.btnGetChatLinks = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colChatLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetChatLinks
            // 
            this.btnGetChatLinks.Location = new System.Drawing.Point(12, 12);
            this.btnGetChatLinks.Name = "btnGetChatLinks";
            this.btnGetChatLinks.Size = new System.Drawing.Size(131, 23);
            this.btnGetChatLinks.TabIndex = 0;
            this.btnGetChatLinks.Text = "Chat-Links abrufen";
            this.btnGetChatLinks.UseVisualStyleBackColor = true;
            this.btnGetChatLinks.Click += new System.EventHandler(this.btnGetChatLinks_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChatLink});
            this.dataGridView1.Location = new System.Drawing.Point(12, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(360, 308);
            this.dataGridView1.TabIndex = 1;
            // 
            // colChatLink
            // 
            this.colChatLink.HeaderText = "Chat-Link";
            this.colChatLink.Name = "colChatLink";
            this.colChatLink.ReadOnly = true;
            this.colChatLink.Width = 300;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnGetChatLinks);
            this.Name = "MainForm";
            this.Text = "Chat-Links Extractor";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
