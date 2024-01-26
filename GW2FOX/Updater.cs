using System;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

public class Updater
{
    private const string RepositoryUrl = "https://api.github.com/repos/DEIN_BENUTZERNAME/DEIN_REPO/releases/latest";
    private const string UpdateFolder = "Update";

    public static void CheckForUpdates(string currentVersion)
    {
        string latestVersion = GetLatestVersionFromGitHub();

        if (latestVersion.CompareTo(currentVersion) > 0)
        {
            Console.WriteLine("Ein Update ist verfügbar.");

            if (DownloadUpdate())
            {
                Console.WriteLine("Update erfolgreich heruntergeladen.");
                InstallUpdate();
            }
            else
            {
                Console.WriteLine("Fehler beim Herunterladen des Updates.");
            }
        }
        else
        {
            Console.WriteLine("Die Anwendung ist auf dem neuesten Stand.");
        }
    }

    private static string GetLatestVersionFromGitHub()
    {
        using (WebClient client = new WebClient())
        {
            client.Headers.Add("User-Agent", "request");
            string json = client.DownloadString(RepositoryUrl);

            // Hier musst du den JSON-Response entsprechend analysieren, um die neueste Version zu extrahieren.
            // Beachte, dass die GitHub-API-Aufrufe Authentifizierung erfordern können.
            // Verwende bevorzugt einen Token für sichere Authentifizierung.

            // Beispiel (vereinfacht):
            // Angenommen, dein JSON enthält ein Feld "tag_name" mit der Versionsnummer.
            dynamic releaseInfo = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            string latestVersion = releaseInfo.tag_name;

            return latestVersion;
        }
    }

    private static bool DownloadUpdate()
    {
        try
        {
            Directory.CreateDirectory(UpdateFolder);

            using (WebClient client = new WebClient())
            {
                string downloadUrl = "URL_DES_HERUNTERLADBAREN_UPDATES"; // Ersetze dies durch die tatsächliche URL des Updates.
                string updateFilePath = Path.Combine(UpdateFolder, "Update.zip");

                client.DownloadFile(downloadUrl, updateFilePath);

                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Herunterladen des Updates: {ex.Message}");
            return false;
        }
    }

    private static void InstallUpdate()
    {
        try
        {
            string updateFilePath = Path.Combine(UpdateFolder, "Update.zip");

            // Hier implementierst du den Code zum Entpacken des Updates und zur Aktualisierung der Anwendung.
            // Je nach Update-Mechanismus kann dies variieren.

            // Beispiel (vereinfacht):
            ZipFile.ExtractToDirectory(updateFilePath, "Pfad_zum_Installationsverzeichnis");

            Console.WriteLine("Update erfolgreich installiert.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Installieren des Updates: {ex.Message}");
        }
    }
}
