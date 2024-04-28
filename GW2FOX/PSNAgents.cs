using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GW2FOX
{
    public partial class PSNAgents : Form
    {
        public PSNAgents()
        {
            InitializeComponent();
        }

        private void btnGetChatLinks_Click(object sender, EventArgs e)
        {
            // URL zur Wiki-Seite mit den NPC-Informationen
            string url = "https://wiki.guildwars2.com/wiki/Template:Pact_Supply_Network_Agent_table";

            // HTML von der Seite herunterladen
            string htmlCode = DownloadHtml(url);

            // Anzeigen des heruntergeladenen HTML-Codes
            MessageBox.Show("Heruntergeladener HTML-Code:\n\n" + htmlCode);

            // Extrahiere die Chat-Links für heute aus dem HTML-Code
            List<string> chatLinks = ExtractChatLinks(htmlCode);

            // Fülle das DataGridView mit den Chat-Links
            dataGridView1.Rows.Clear();
            foreach (string link in chatLinks)
            {
                dataGridView1.Rows.Add(link);
            }

            // DataGridView aktualisieren
            dataGridView1.Refresh();
        }

        private string DownloadHtml(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                try
                {
                    // User-Agent setzen, um sich als Webbrowser auszugeben
                    client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");

                    return client.DownloadString(url);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Herunterladen der Seite: " + ex.Message);
                    return string.Empty;
                }
            }
        }

        private List<string> ExtractChatLinks(string html)
        {
            List<string> chatLinks = new List<string>();

            // Muster für Chat-Links
            string pattern = @"<td data-location=""[^""]+""><span class=""gamelink"" id=""[^""]+"" data-type=""[^""]+"" data-id=""[^""]+"">[^<]+<\/span><script>\(function \(t\)\s*{.*?}(?<=(?<!\\)'gamelink-\d+')(.*?)}<\/script><\/td>";

            // Anzeigen des verwendeten Regex-Patterns
            MessageBox.Show("Verwendetes Regex-Pattern:\n\n" + pattern);

            // Regex-Matches im HTML suchen
            MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.Singleline);

            // Debug-Ausgabe der Match-Anzahl
            MessageBox.Show("Anzahl der Matches: " + matches.Count);

            // Jedes Match zu den Chat-Links hinzufügen
            foreach (Match match in matches)
            {
                string chatLink = match.Groups[0].Value;

                // HTML-Tags entfernen und nur den Chat-Link behalten
                chatLink = Regex.Replace(chatLink, @"<[^>]+>|&nbsp;", "").Trim();

                // Debug-Ausgabe jedes Chat-Links
                MessageBox.Show("Chat-Link: " + chatLink);

                chatLinks.Add(chatLink);
            }

            return chatLinks;
        }
    }
}
