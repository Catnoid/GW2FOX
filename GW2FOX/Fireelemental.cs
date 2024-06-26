﻿using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GW2FOX
{
    public partial class Fireelemental : BaseForm
    {
        public Fireelemental()
        {
            InitializeComponent();
            LoadConfigText(Runinfo, Squadinfo, Guild, Welcome, Symbols);
        }












        private void Runinfo_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Runinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Squadinfo_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Squadinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Guild_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Guild.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Welcome_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Welcome.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Fireinfo_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Fireinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Fireinstance_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Fireinstance.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Attentionfire_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Attentionfire.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Fireextra_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Firextra.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Matrixkey_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Matrixkey.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string homepageUrl = "https://wiki.guildwars2.com/wiki/Destroy_the_fire_elemental_created_from_chaotic_energy_fusing_with_the_C.L.E.A.N._5000's_energy_core";
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = homepageUrl,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim ?ffnen der Homepage: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}