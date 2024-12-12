using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GW2FOX
{
    public partial class ButtonFox : BaseForm
    {
        private Worldbosses worldbossesWindow;
        // Importieren der notwendigen Windows-API-Funktionen
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_RESTORE = 9; // Restore a minimized window

        public ButtonFox()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (worldbossesWindow == null || worldbossesWindow.IsDisposed) // Prüfen, ob Instanz existiert
                {
                    worldbossesWindow = new Worldbosses
                    {
                        Owner = this,
                        WindowState = FormWindowState.Maximized, // Vollbildmodus
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
                string homepageUrl = "https://wiki.guildwars2.com";
                Process browserProcess = Process.Start(new ProcessStartInfo
                {
                    FileName = homepageUrl,
                    UseShellExecute = true
                });

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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Suchen des Fensters mit dem Titel "Rechner"
                IntPtr calcWindow = FindWindow("ApplicationFrameWindow", "Rechner");

                if (calcWindow != IntPtr.Zero)
                {
                    // Fenster gefunden, wiederherstellen und in den Vordergrund bringen
                    ShowWindow(calcWindow, SW_RESTORE);
                    SetForegroundWindow(calcWindow);
                }
                else
                {
                    // Taschenrechner starten, wenn nicht gefunden
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "calc.exe",
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Öffnen des Taschenrechners: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // Suchen des Fensters mit dem Titel "Snipping Tool"
                IntPtr snippingToolWindow = FindWindow(null, "Snipping Tool");

                if (snippingToolWindow != IntPtr.Zero)
                {
                    // Fenster gefunden, wiederherstellen und in den Vordergrund bringen
                    ShowWindow(snippingToolWindow, SW_RESTORE);
                    SetForegroundWindow(snippingToolWindow);
                }
                else
                {
                    // Snipping Tool starten, wenn nicht gefunden
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "snippingtool.exe", // Snipping Tool unter Windows 11
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Öffnen des Snipping Tools: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
