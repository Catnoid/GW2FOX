using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GW2FOX
{
    public partial class ButtonFox : BaseForm
    {
        private Worldbosses worldbossesWindow;

        public ButtonFox()
        {
            InitializeComponent();

            // Initialisiere die Form für Overlay-Zwecke
            this.TopMost = true; // Immer im Vordergrund
            this.FormBorderStyle = FormBorderStyle.None; // Ohne Rahmen
            this.StartPosition = FormStartPosition.Manual; // Benutzerdefinierte Position
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black; // Transparenz
            this.Opacity = 0.8; // Optionale Transparenz für das Overlay
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (worldbossesWindow == null || worldbossesWindow.IsDisposed) // Prüfen, ob Instanz existiert
                {
                    worldbossesWindow = new Worldbosses
                    {
                        Owner = this // Setzt das aktuelle Fenster als Besitzer
                    };
                    worldbossesWindow.Show(); // Neues Fenster öffnen
                }
                else
                {
                    // Falls bereits geöffnet, Fenster in den Vordergrund bringen
                    worldbossesWindow.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Öffnet die URL in einem neuen Browser-Fenster
                string homepageUrl = "https://wiki.guildwars2.com";
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = homepageUrl,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Default path to Discord's Update.exe
                string discordPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Discord", "Update.exe");

                if (File.Exists(discordPath))
                {
                    // Launch Discord
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = discordPath,
                        Arguments = "--processStart Discord.exe", // Starts the main application
                        UseShellExecute = true
                    };
                    Process.Start(startInfo);
                }
                else
                {
                    MessageBox.Show("Discord could not be found. Please check if it is installed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while starting Discord: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Verzeichnis der ausführbaren Datei erhalten
            string exeDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            // Pfad zur Datei "Blish HUD.exe" im Verzeichnis "data"
            string filePath = Path.Combine(exeDirectory, "data", "GW2TacO.exe");

            // Überprüfen, ob die Datei existiert, bevor sie geöffnet wird
            if (File.Exists(filePath))
            {
                try
                {
                    // Öffne die Datei
                    Process.Start(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen der Datei: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Die Datei wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Verzeichnis der ausführbaren Datei erhalten
            string exeDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            // Pfad zur Datei "Blish HUD.exe" im Verzeichnis "data"
            string filePath = Path.Combine(exeDirectory, "data2", "Blish HUD.exe");

            // Überprüfen, ob die Datei existiert, bevor sie geöffnet wird
            if (File.Exists(filePath))
            {
                try
                {
                    // Öffne die Datei
                    Process.Start(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Öffnen der Datei: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Die Datei wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
