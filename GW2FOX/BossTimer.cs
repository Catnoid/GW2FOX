using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GW2FOX.BossTimings;

namespace GW2FOX
{
    public class BossTimer : IDisposable
    {
        private static readonly string TimeZoneId = "W. Europe Standard Time";
        private static readonly Color DefaultFontColor = Color.Blue;
        private static readonly Color PastBossFontColor = Color.OrangeRed;

        private readonly ListView bossList;
        private readonly TimeZoneInfo mezTimeZone;
        private readonly System.Threading.Timer timer;
        public bool IsRunning { get; private set; }

        public BossTimer(ListView bossList)
        {
            this.bossList = bossList;
            mezTimeZone = TimeZoneInfo.Local; // Verwende die lokale Zeitzone des Systems
            timer = new System.Threading.Timer(TimerCallback, null, 0, 1000);
            IsRunning = false;
        }


        public void Start()
        {
            timer.Change(0, 1000);
            IsRunning = true;
        }

        public void Stop()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            IsRunning = false;
        }

        private void TimerCallback(object? state)
        {
            try
            {
                UpdateBossList();
            }
            catch (Exception ex)
            {
                HandleException(ex, "TimerCallback");
            }
        }

        public void UpdateBossList()
        {
            if (!bossList.IsHandleCreated) return;

            bossList.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    bossList.BeginUpdate();

                    var tempPreviousBosses = UpdateBossListPreviousBosses();
                    var tempUpcomingBosses = UpdateBossListUpcomingBosses();

                    bossList.Items.Clear();
                    bossList.Items.AddRange(tempPreviousBosses.ToArray());
                    bossList.Items.AddRange(tempUpcomingBosses.ToArray());
                }
                catch (Exception ex)
                {
                    HandleException(ex, "UpdateBossList");
                }
                finally
                {
                    bossList.EndUpdate();
                }
            });
        }

        private List<ListViewItem> UpdateBossListUpcomingBosses()
        {
            var tempUpcomingBosses = new List<ListViewItem>();

            try
            {
                List<string> bossNamesFromConfig = BossList23;

                DateTime currentTimeUtc = DateTime.UtcNow;
                DateTime currentTimeLocal = TimeZoneInfo.ConvertTimeFromUtc(currentTimeUtc, mezTimeZone);

                var upcomingBosses = BossEventGroups
                    .Where(bossEventGroup => bossNamesFromConfig.Contains(bossEventGroup.BossName))
                    .SelectMany(bossEventGroup => bossEventGroup.GetNextRuns())
                    .OrderBy(bossEvent => bossEvent.NextRunTime)
                    .ToList();

                // Verwendete BossEvents, um Duplikate zu verfolgen
                HashSet<string> usedBossEvents = new HashSet<string>();

                foreach (var bossEvent in upcomingBosses)
                {
                    // Erzeuge einen eindeutigen Schlüssel für das BossEvent
                    string bossEventKey = $"{bossEvent.BossName}_{bossEvent.Category}_{bossEvent.Timing}";

                    Color fontColor = GetFontColor(bossEvent);

                    // Check if the countdown has ended
                    if (bossEvent.NextRunTime <= currentTimeLocal)
                    {
                        // Treat as if it were a previous boss for the specified duration
                        var remainingTime = currentTimeLocal - bossEvent.NextRunTime;
                        if (remainingTime.TotalMinutes <= 14.99)
                        {
                            var listViewItemExpired = new ListViewItem(new[] { bossEvent.BossName, "" });
                            listViewItemExpired.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            listViewItemExpired.ForeColor = PastBossFontColor;
                            tempUpcomingBosses.Add(listViewItemExpired);
                        }
                        continue;  // Skip adding this boss to the regular upcoming bosses list
                    }

                    // Adjust currentTimeLocal to the next day if needed
                    if (bossEvent.NextRunTime <= currentTimeLocal)
                    {
                        currentTimeLocal = currentTimeLocal.AddDays(1);
                    }

                    // Überprüfe, ob das BossEvent bereits verwendet wurde
                    if (usedBossEvents.Contains(bossEventKey))
                    {
                        // Wenn bereits verwendet, füge das BossEvent mit 24 Stunden Verzögerung hinzu
                        var delayedBossEvent = bossEvent.Clone(); // Annahme: BossEvent ist klonbar

                        // If the boss event is scheduled after midnight, adjust the timing to the next day
                        if (delayedBossEvent.NextRunTime.TimeOfDay < currentTimeLocal.TimeOfDay)
                        {
                            delayedBossEvent.NextRunTime = delayedBossEvent.NextRunTime.AddDays(1);
                        }

                        // Generate a unique key for the delayed BossEvent
                        string delayedBossEventKey = $"{delayedBossEvent.BossName}_{delayedBossEvent.Category}_{delayedBossEvent.Timing}";

                        // Check if the delayed BossEvent has already been used
                        if (!usedBossEvents.Contains(delayedBossEventKey))
                        {
                            var remainingBossTime = delayedBossEvent.NextRunTime - currentTimeLocal;
                            var formattedRemainingTime = $"{(int)remainingBossTime.TotalHours:D2}:{remainingBossTime.Minutes:D2}:{remainingBossTime.Seconds:D2}";

                            var listViewItemDelayed = new ListViewItem(new[] {
                        delayedBossEvent.BossName,
                        formattedRemainingTime
                    });

                            listViewItemDelayed.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            listViewItemDelayed.ForeColor = fontColor;
                            tempUpcomingBosses.Add(listViewItemDelayed);

                            // Markiere das BossEvent und das verschobene BossEvent als verwendet
                            usedBossEvents.Add(bossEventKey);
                            usedBossEvents.Add(delayedBossEventKey);
                        }
                    }
                    else
                    {
                        // Wenn nicht verwendet, füge das BossEvent sofort hinzu
                        var remainingBossTime = bossEvent.NextRunTime - currentTimeLocal;
                        var formattedRemainingTime = $"{(int)remainingBossTime.TotalHours:D2}:{remainingBossTime.Minutes:D2}:{remainingBossTime.Seconds:D2}";

                        var listViewItem = new ListViewItem(new[] {
                    bossEvent.BossName,
                    formattedRemainingTime
                });

                        if (HasSameNameAndCategory(upcomingBosses, bossEvent))
                        {
                            listViewItem.Font = new Font("Segoe UI", 10, FontStyle.Italic | FontStyle.Bold);
                        }
                        else
                        {
                            listViewItem.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        }

                        listViewItem.ForeColor = fontColor;
                        tempUpcomingBosses.Add(listViewItem);

                        // Markiere das BossEvent als verwendet
                        usedBossEvents.Add(bossEventKey);

                        // Add the "24h later" timing below the current timing
                        var delayedBossEvent = bossEvent.Clone(); // Annahme: BossEvent ist klonbar
                        delayedBossEvent.NextRunTime = delayedBossEvent.NextRunTime.AddHours(24);

                        // Generate a unique key for the delayed BossEvent
                        string delayedBossEventKey = $"{delayedBossEvent.BossName}_{delayedBossEvent.Category}_{delayedBossEvent.Timing}";

                        // Check if the delayed BossEvent has already been used
                        if (!usedBossEvents.Contains(delayedBossEventKey))
                        {
                            var remainingBossTimeDelayed = delayedBossEvent.NextRunTime - currentTimeLocal;
                            var formattedRemainingTimeDelayed = $"{(int)remainingBossTimeDelayed.TotalHours:D2}:{remainingBossTimeDelayed.Minutes:D2}:{remainingBossTimeDelayed.Seconds:D2}";

                            var listViewItemDelayed = new ListViewItem(new[] {
                        delayedBossEvent.BossName,
                        formattedRemainingTimeDelayed
                    });

                            listViewItemDelayed.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            listViewItemDelayed.ForeColor = fontColor;
                            tempUpcomingBosses.Add(listViewItemDelayed);

                            // Markiere das verschobene BossEvent als verwendet
                            usedBossEvents.Add(delayedBossEventKey);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "UpdateBossListUpcomingBosses");
            }

            return tempUpcomingBosses;
        }




        private List<ListViewItem> UpdateBossListPreviousBosses()
        {
            var tempPreviousBosses = new List<ListViewItem>();

            try
            {
                // Bossnamen aus der Konfiguration laden
                List<string> bossNamesFromConfig = BossList23;

                // Aktuelle Zeit abrufen
                DateTime currentTime = DateTime.Now;

                // Vorherige Bosse abrufen
                var previousBosses = BossEventGroups
                    .Where(bossEventGroup => bossNamesFromConfig.Contains(bossEventGroup.BossName))
                    .SelectMany(bossEventGroup => bossEventGroup.GetPreviousRuns())
                    .ToList();

                foreach (var bossEvent in previousBosses)
                {
                    // Calculate the time difference between the boss event and the current time
                    TimeSpan remainingBossTime = currentTime - bossEvent.NextRunTime;

                    // Check if the event is within the last 14.59 minutes and not more than 0 minutes (not in the future)
                    if (remainingBossTime.TotalMinutes <= 14.59 && remainingBossTime.TotalMinutes >= 0)
                    {
                        // Eindeutigen Schlüssel für den Bossnamen und den Zeitpunkt erstellen
                        string bossNameKey = $"{bossEvent.BossName}_{bossEvent.NextRunTime}";

                        // Farbe für den Text festlegen
                        Color fontColor = GetFontColor(bossEvent);

                        // ListViewItem erstellen und zur Liste hinzufügen
                        var listViewItem = new ListViewItem(new[] { bossEvent.BossName });
                        listViewItem.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        listViewItem.SubItems.Add(""); // Leeres SubItem hinzufügen (falls benötigt)
                        listViewItem.ForeColor = fontColor;

                        tempPreviousBosses.Add(listViewItem);
                    }
                }
            }
            catch (Exception ex)
            {
                // Fehler behandeln und protokollieren
                HandleException(ex, "UpdateBossListPreviousBosses");
            }

            return tempPreviousBosses;
        }


        private bool HasSameNameAndCategory(List<BossEventRun> upcomingBosses, BossEventRun currentBossEvent)
        {
            return upcomingBosses.Any(bossEvent =>
                bossEvent != currentBossEvent &&
                bossEvent.Category == currentBossEvent.Category &&
                bossEvent.BossName == currentBossEvent.BossName &&
                bossEvent.Timing == currentBossEvent.Timing &&
                bossEvent.NextRunTime != currentBossEvent.NextRunTime);
        }



        private DateTime GetAdjustedTiming(DateTime currentTimeMez, TimeSpan bossTiming)
        {
            DateTime adjustedTiming = currentTimeMez.Date + bossTiming;
            return adjustedTiming;
        }


        private void HandleException(Exception ex, string methodName)
        {
            Console.WriteLine($"Exception in {methodName}: {ex}");
            // Consider logging the exception with more details
        }

        private TimeSpan GetRemainingTime(DateTime currentTimeMez, DateTime adjustedTiming, BossEventRun bossEvent)
        {
            return adjustedTiming - currentTimeMez;
        }

        private static Color GetFontColor(BossEventRun bossEvent)
        {
            Color fontColor;

            // Überprüfe, ob das BossEvent zu den PreviewBosses gehört
            if (bossEvent.IsPreviewBoss)
            {
                fontColor = PastBossFontColor; // Setze die Farbe auf OrangeRed für PreviewBosses
            }

            else
            {
                // Setze die Farbe basierend auf der Kategorie des BossEvents
                switch (bossEvent.Category)
                {
                    case "Maguuma":
                        fontColor = Color.LimeGreen;
                        break;
                    case "Desert":
                        fontColor = Color.DeepPink;
                        break;
                    case "WBs":
                        fontColor = Color.WhiteSmoke;
                        break;
                    case "Ice":
                        fontColor = Color.DeepSkyBlue;
                        break;
                    case "Cantha":
                        fontColor = Color.Blue;
                        break;
                    case "SotO":
                        fontColor = Color.Yellow;
                        break;
                    case "LWS2":
                        fontColor = Color.LightYellow;
                        break;
                    case "LWS3":
                        fontColor = Color.ForestGreen;
                        break;
                    default:
                        fontColor = DefaultFontColor;
                        break;
                }
            }

            return fontColor;
        }




        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (timer != null)
                {
                    timer.Dispose();
                }
            }
        }

        ~BossTimer()
        {
            Dispose(false);
        }
    }
}


