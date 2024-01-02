using GW2FOX;
using System;
using System.Windows.Forms;

namespace GW2FOX
{
    internal static class Program
        {
            private static Overlay overlayForm;
            private static BossTimer bossTimer;
            private static Main mainForm;

            [STAThread]
        static void Main()
        {   
            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
        private static void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
                // Stoppe den BossTimer, wenn die Main Form geschlossen wird
        bossTimer.StopTimer();

                // Schlie�e das Overlay Form
        overlayForm.CloseOverlay();

                // Schlie�e die Main Form
        mainForm.Close();
        }
    }
}