using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GW2FOX
{
    public partial class PSNA : BaseForm
    {
        // Dictionary für Wochentage und zugehörige NPC-Namen und Chat-Links
        private Dictionary<string, Dictionary<string, string>> npcChatLinksByDay = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "Monday", new Dictionary<string, string>
                {
                    { "Mehem the Traveled", "[&BA8CAAA=]" },
                    { "The Fox", "[&BKYBAAA=]" },
                    { "Specialist Yana", "[&BEwDAAA=]" },
                    { "Lady Derwena", "[&BIcHAAA=]" },
                    { "Despina Katelyn", "[&BNIEAAA=]" },
                    { "Verma Giftrender", "[&BIMCAAA=]" }
                }
            },
            {
                "Tuesday", new Dictionary<string, string>
                {
                    { "Mehem the Traveled", "[&BIMBAAA=]" },
                    { "The Fox", "[&BBkAAAA=]" },
                    { "Specialist Yana", "[&BEgAAAA=]" },
                    { "Lady Derwena", "[&BH8HAAA=]" },
                    { "Despina Katelyn", "[&BKgCAAA=]" },
                    { "Verma Giftrender", "[&BGQCAAA=]" }
                }
            },
            {
                "Wednesday", new Dictionary<string, string>
                {
                    { "Mehem the Traveled", "[&BPEBAAA=]" },
                    { "The Fox", "[&BKYAAAA=]" },
                    { "Specialist Yana", "[&BMIBAAA=]" },
                    { "Lady Derwena", "[&BH4HAAA=]" },
                    { "Despina Katelyn", "[&BP0CAAA=]" },
                    { "Verma Giftrender", "[&BDgDAAA=]" }
                }
            },
            {
                "Thursday", new Dictionary<string, string>
                {
                    { "Mehem the Traveled", "[&BOcBAAA=]" },
                    { "The Fox", "[&BIMAAAA=]" },
                    { "Specialist Yana", "[&BF0AAAA=]" },
                    { "Lady Derwena", "[&BKsHAAA=]" },
                    { "Despina Katelyn", "[&BO4CAAA=]" },
                    { "Verma Giftrender", "[&BF0GAAA=]" }
                }
            },
            {
                "Friday", new Dictionary<string, string>
                {
                    { "Mehem the Traveled", "[&BJQHAAA=]" },
                    { "The Fox", "[&BMMCAAA=]" },
                    { "Specialist Yana", "[&BJsCAAA=]" },
                    { "Lady Derwena", "[&BNUGAAA=]" },
                    { "Despina Katelyn", "[&BHsBAAA=]" },
                    { "Verma Giftrender", "[&BNMAAAA=]" }
                }
            },
            {
                "Saturday", new Dictionary<string, string>
                {
                    { "Mehem the Traveled", "[&BBABAAA=]" },
                    { "The Fox", "[&BJIBAAA=]" },
                    { "Specialist Yana", "[&BLkCAAA=]" },
                    { "Lady Derwena", "[&BH8HAAA=]" },
                    { "Despina Katelyn", "[&BBEDAAA=]" },
                    { "Verma Giftrender", "[&BEICAAA=]" }
                }
            },
            {
                "Sunday", new Dictionary<string, string>
                {
                    { "Mehem the Traveled", "[&BCECAAA=]" },
                    { "The Fox", "[&BC0AAAA=]" },
                    { "Specialist Yana", "[&BDoBAAA=]" },
                    { "Lady Derwena", "[&BIkHAAA=]" },
                    { "Despina Katelyn", "[&BO4CAAA=]" },
                    { "Verma Giftrender", "[&BIUCAAA=]" }
                }
            }
        };

        public PSNA()
        {
            InitializeComponent();

            // Bestimme den aktuellen Wochentag und die aktuelle Uhrzeit in MEZ
            DateTime now = DateTime.UtcNow.AddHours(2); // Berücksichtige die MEZ (UTC+2)
            string currentDay = now.DayOfWeek.ToString();
            int currentHour = now.Hour;

            // Wenn es vor 10 Uhr ist, nutze den vorherigen Tag
            if (currentHour < 10)
            {
                now = now.AddDays(-1);
                currentDay = now.DayOfWeek.ToString();
            }

            // Text für das Textfeld txtAgentLocations erstellen
            string message = "";

            // Überprüfe, ob Chat-Links für den aktuellen Wochentag vorhanden sind
            if (npcChatLinksByDay.ContainsKey(currentDay))
            {
                // Extrahiere die NPC-Namen und Chat-Links für den aktuellen Wochentag
                Dictionary<string, string> npcChatLinks = npcChatLinksByDay[currentDay];

                // Baue die Nachricht mit den NPC-Namen und Chat-Links
                bool first = true;
                foreach (var npc in npcChatLinks)
                {
                    if (!first)
                    {
                        // Füge ein Komma und Leerzeichen hinzu, außer für das erste Element
                        message += ", ";
                    }
                    message += $"{npc.Key} - {npc.Value}";
                    first = false;
                }
            }
            else
            {
                message = $"Keine NPC gefunden für den aktuellen Wochentag: {currentDay}.";
            }

            // Text im Textfeld txtAgentLocations setzen
            txtAgentLocations.Text = message;

            // Debug-Ausgabe
            Console.WriteLine("Aktueller Wochentag: " + currentDay);
            Console.WriteLine("Aktuelle Uhrzeit (MEZ): " + now.ToString("HH:mm"));
        }

        public string GetNPCData()
        {
            try
            {
                DateTime now = DateTime.UtcNow.AddHours(2); // Berücksichtige die MEZ (UTC+2)
                string currentDay = now.DayOfWeek.ToString();
                int currentHour = now.Hour;

                if (currentHour < 10)
                {
                    now = now.AddDays(-1);
                    currentDay = now.DayOfWeek.ToString();
                }

                string message = "";

                if (npcChatLinksByDay.ContainsKey(currentDay))
                {
                    Dictionary<string, string> npcChatLinks = npcChatLinksByDay[currentDay];

                    bool first = true;
                    foreach (var npc in npcChatLinks)
                    {
                        if (!first)
                        {
                            message += ", ";
                        }
                        message += $"{npc.Key} - {npc.Value}";
                        first = false;
                    }
                }
                else
                {
                    message = $"Keine NPC gefunden für den aktuellen Wochentag: {currentDay}.";
                }

                return message;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Abrufen der Daten: {ex.Message}");
                return "";
            }
        }
    }
}