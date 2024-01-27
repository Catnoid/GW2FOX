// Main.cs

using System.Diagnostics;
using IWshRuntimeLibrary;
using File = System.IO.File;

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
            BossTimerService.Timer_Click(sender, e);
            // Additional logic specific to Timer_Click in Main class, if any
        }


        private void HandleException(Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}\n\nStack Trace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OpenForm(Form newForm)
        {
            newForm.Owner = this;
            newForm.Show();
            // this.Dispose();
        }

        private void Fox_Click(object sender, EventArgs e)
        {
            try
            {
                string homepageUrl = "https://gw2-hub.000webhostapp.com/";
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

        private void Uam_Click(object sender, EventArgs e)
        {
            try
            {
                string homepageUrl = "https://github.com/gw2-addon-loader/GW2-Addon-Manager/releases";
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

        private void Leading_Click(object sender, EventArgs e)
        {
            ShowAndHideForm(new Worldbosses());
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

    }
}
