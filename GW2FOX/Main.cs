// Main.cs

using System.Diagnostics;
using IWshRuntimeLibrary;
using File = System.IO.File;
using System.Net;

namespace GW2FOX
{
    public partial class Main : BaseForm
    {


        private GlobalKeyboardHook? _globalKeyboardHook; // Füge dies hinzu

        public Main()
        {
            InitializeComponent();
            Load += Main_Load;

            InitializeGlobalKeyboardHook();

            // Updater.CheckForUpdates(Worldbosses.getConfigLineForItem("Version"));
        }


        private void InitializeGlobalKeyboardHook()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyPressed += GlobalKeyboardHook_KeyPressed;
        }

        private void GlobalKeyboardHook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (ModifierKeys == Keys.Alt && e.Key == Keys.T)
            {
                Timer_Click(sender, e);
            }
        }

        private void Main_Load(object? sender, EventArgs e)
        {
            try
            {
                AdjustWindowSize();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }


        private void Timer_Click(object sender, EventArgs e)
        {
            try
            {
                // Öffnet die ButtonFox-Form
                OpenButtonFoxOverlay();

                // Führt die bestehende Logik für den Timer aus
                BossTimerService.Timer_Click(sender, e);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void OpenButtonFoxOverlay()
        {
            // Instanziiere und zeige die ButtonFox-Form als Overlay
            ButtonFox overlay = new ButtonFox
            {
                TopMost = true, // Immer im Vordergrund
                FormBorderStyle = FormBorderStyle.None, // Ohne Rahmen
                StartPosition = FormStartPosition.Manual, // Benutzerdefinierte Position
                BackColor = Color.Black,
                TransparencyKey = Color.Black, // Macht die Form transparent
                Opacity = 0.8, // Optionale Transparenz
                Width = 210, // Breite des Overlays
                Height = 40 // Höhe des Overlays
            };

            // Position: 20 cm (756 px) vom linken Rand, oben bündig
            overlay.Location = new Point(756, 0);

            overlay.Show();
        }



        private void HandleException(Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}\n\nStack Trace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OpenForm(Form newForm)
        {
            newForm.Owner = this;
            newForm.Show();
        }

        private void Fox_Click(object sender, EventArgs e)
        {
            try
            {
                string homepageUrl = "https://www.gw2fox.com";
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

        private void Repair_Click(object sender, EventArgs e)
        {
            string gw2ExecutablePath = GetGw2ExecutablePath();
            if (string.IsNullOrEmpty(gw2ExecutablePath))
            {
                MessageBox.Show("No Gw2-64.exe selected! Please choose the Guild Wars 2 executable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string gw2Directory = Path.GetDirectoryName(gw2ExecutablePath);
            string shortcutPath = Path.Combine(gw2Directory, "RepairGW2.lnk");

            CreateShortcut(gw2ExecutablePath, shortcutPath, "-repair");

            LaunchWithRepair(shortcutPath);
        }

        private string GetGw2ExecutablePath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Guild Wars 2 Executable|Gw2-64.exe|All Files|*.*";
                openFileDialog.Title = "Select Guild Wars 2 Executable (Gw2-64.exe)";

                return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
            }
        }

        private void CreateShortcut(string targetPath, string shortcutPath, string commandLineParameters)
        {
            try
            {
                if (File.Exists(shortcutPath))
                {
                    MessageBox.Show("Shortcut already exists. Launching existing shortcut.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.TargetPath = targetPath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath); // Setzen Sie das Arbeitsverzeichnis auf den Ordner der ausf?hrbaren Datei
                shortcut.Arguments = commandLineParameters;
                shortcut.Save(); // Speichern Sie die Verkn?pfung, um sie zu erstellen
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating the shortcut: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LaunchWithRepair(string shortcutPath)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = shortcutPath,
                    UseShellExecute = true,
                    Verb = "open"
                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error launching the shortcut: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseAll_Click(object sender, EventArgs e)
        {
            try
            {
                // Stop the Timer and dispose of the BossTimer
                BossTimerService._bossTimer?.Stop();
                BossTimerService._bossTimer?.Dispose(); // Dispose of the BossTimer

                // Close the Overlay and dispose of it
                BossTimerService._overlay?.Close();
                BossTimerService._overlay?.Dispose(); // Dispose of the Overlay

                // Close the program
                Application.Exit();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void ReShade_Click(object sender, EventArgs e)
        {
            try
            {
                string homepageUrl = "https://discord.com/download";
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

        private void ArcDPSInstall_Click(object sender, EventArgs e)
        {
            string gw2Verzeichnis = GetGw2Verzeichnis();

            if (string.IsNullOrEmpty(gw2Verzeichnis))
            {
                MessageBox.Show("Error - nothing is chooooooosen!.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            InstallArcDPS(gw2Verzeichnis);
        }

        private string GetGw2Verzeichnis()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Please select the directory of Guild Wars 2 where the .exe file is located.";
                dialog.ShowNewFolderButton = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
            }

            return null;
        }

        private void InstallArcDPS(string gw2Verzeichnis)
        {
            // URL zum ArcDPS-Verzeichnis
            string arcDPSBaseUrl = "https://www.deltaconnected.com/arcdps/x64/";

            // Dateinamen
            string d3d11DllName = "d3d11.dll";
            string d3d11Md5SumName = "d3d11.dll.md5sum";

            // Ziel Pfade im Guild Wars 2-Verzeichnis
            string d3d11DllZiel = Path.Combine(gw2Verzeichnis, d3d11DllName);
            string d3d11Md5SumZiel = Path.Combine(gw2Verzeichnis, d3d11Md5SumName);

            try
            {
                // Dateien herunterladen
                WebClient client = new WebClient();
                client.DownloadFile(arcDPSBaseUrl + d3d11DllName, d3d11DllZiel);
                client.DownloadFile(arcDPSBaseUrl + d3d11Md5SumName, d3d11Md5SumZiel);

                MessageBox.Show("ArcDPS wurde erfolgreich installiert.", "Installation abgeschlossen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Installieren von ArcDPS: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ArcDPSDeinstall_Click(object sender, EventArgs e)
        {
            string gw2Verzeichnis = GetGw2Verzeichnis();

            if (string.IsNullOrEmpty(gw2Verzeichnis))
            {
                MessageBox.Show("The Guild Wars 2 directory was not selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Pfade zu den zu löschenden Dateien
                string d3d11DllZiel = Path.Combine(gw2Verzeichnis, "d3d11.dll");
                string d3d11Md5SumZiel = Path.Combine(gw2Verzeichnis, "d3d11.dll.md5sum");

                // Dateien löschen, wenn sie existieren
                if (File.Exists(d3d11DllZiel))
                    File.Delete(d3d11DllZiel);

                if (File.Exists(d3d11Md5SumZiel))
                    File.Delete(d3d11Md5SumZiel);

                MessageBox.Show("Done.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
