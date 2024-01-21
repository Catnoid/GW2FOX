using System.Diagnostics;
using static GW2FOX.BossTimings;
using static GW2FOX.GlobalVariables;

namespace GW2FOX
{
    public class BaseForm : Form
    {

        protected Overlay overlay;
        protected ListView customBossList;
        protected BossTimer bossTimer;
        private GlobalKeyboardHook? _globalKeyboardHook;

        public static ListView CustomBossList { get; private set; } = new ListView();



        public BaseForm()
        {
            InitializeCustomBossList();
            overlay = new Overlay(customBossList);
            bossTimer = new BossTimer(customBossList);
            InitializeGlobalKeyboardHook();
        }

        protected void InitializeBossTimerAndOverlay()
        {
            bossTimer = new BossTimer(customBossList);
            overlay = new Overlay(customBossList);
            overlay.WindowState = FormWindowState.Normal;
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
                if (this is Main)
                {
                    Timer_Click(sender, e);
                }
            }
        }

        protected void InitializeCustomBossList()
        {
            customBossList = new ListView();
            customBossList.View = View.Details;
            customBossList.Columns.Add("Boss Name", 145);
            customBossList.Columns.Add("Time", 78);
            customBossList.Location = new Point(0, 0);
            customBossList.ForeColor = Color.White;
            new Font("Segoe UI", 10);
        }

        public void UpdateCustomBossList(ListView updatedList)
        {
            CustomBossList = updatedList;
        }

        public void Timer_Click(object sender, EventArgs e)
        {
            
            if (bossTimer != null && bossTimer.IsRunning)
            {
               
                return;
            }

            InitializeCustomBossList();
            InitializeBossTimerAndOverlay();

            bossTimer.Start();
            overlay.Show();
        }


        protected void ShowAndHideForm(Form newForm)
        {
            newForm.Owner = this;
            newForm.Show();
            this.Hide();
            
        }

        protected static void SaveTextToFile(string textToSave, string sectionHeader, bool hideMessages = false)
        {
            var headerToUse = sectionHeader;
            if (headerToUse.EndsWith(':'))
            {
                headerToUse = headerToUse[..^1];
            }

            try
            {
                // Vorhandenen Inhalt aus der Datei lesen
                string[] lines = File.ReadAllLines(FILE_PATH);

                // Index der Zeile mit dem angegebenen Header finden
                int headerIndex = -1;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(headerToUse + ":"))
                    {
                        headerIndex = i;
                        break;
                    }
                }

                // Wenn der Header gefunden wird, den Text aktualisieren
                if (headerIndex != -1)
                {
                    lines[headerIndex] = $"{headerToUse}: \"{textToSave}\"";
                }
                else
                {
                    // Wenn der Header nicht gefunden wird, eine neue Zeile hinzufügen
                    lines = lines.Concat(new[] { $"{headerToUse}: \"{textToSave}\"" }).ToArray();
                }

                // Aktualisierten Inhalt zurück in die Datei schreiben
                File.WriteAllLines(FILE_PATH, lines);

                if (!hideMessages)
                {
                    MessageBox.Show($"{headerToUse} saved.", "Saved!", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {headerToUse}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void AdjustWindowSize()
        {
            Screen currentScreen = Screen.FromControl(this);
            Rectangle workingArea = currentScreen.WorkingArea;

            if (Width > workingArea.Width || Height > workingArea.Height)
            {
                Size = new Size(
                    Math.Min(Width, workingArea.Width),
                    Math.Min(Height, workingArea.Height)
                );

                Location = new Point(
                    workingArea.Left + (workingArea.Width - Width) / 2,
                    workingArea.Top + (workingArea.Height - Height) / 2
                );
            }

            if (Height > workingArea.Height)
            {
                Height = workingArea.Height;
            }

            if (Bottom > workingArea.Bottom)
            {
                Location = new Point(
                    Left,
                    Math.Max(workingArea.Top, workingArea.Bottom - Height)
                );
            }
        }
        protected void HandleException(Exception ex, string methodName)
        {
            Console.WriteLine($"Exception in {methodName}: {ex}");
        }

        private void LoadTextFromConfig(string sectionHeader, TextBox textBox, string configText,
            string defaultToInsert)
        {
            // Suchmuster für den Abschnitt und den eingeschlossenen Text in Anführungszeichen
            string pattern = $@"{sectionHeader}\s*""([^""]*)""";

            // Mit einem regulären Ausdruck nach dem Muster suchen
            var match = System.Text.RegularExpressions.Regex.Match(configText, pattern);

            // Überprüfen, ob ein Treffer gefunden wurde
            if (match.Success)
            {
                // Den extrahierten Text in das Textfeld einfügen
                textBox.Text = match.Groups[1].Value;
            }
            else
            {
                SaveTextToFile(defaultToInsert, sectionHeader, true);
                configText = File.ReadAllText(FILE_PATH);
                LoadTextFromConfig(sectionHeader, textBox, configText, defaultToInsert);
                // Muster wurde nicht gefunden
                // MessageBox.Show($"Das Muster '{sectionHeader}' wurde in der Konfigurationsdatei nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        protected void BringGw2ToFront()
        {
            try
            {
                // Specify the process name without the file extension
                string processName = "Gw2-64";

                // Get the processes by name
                Process[] processes = Process.GetProcessesByName(processName);

                if (processes.Length > 0)
                {
                    // Bring the first instance to the foreground
                    IntPtr mainWindowHandle = processes[0].MainWindowHandle;
                    ShowWindow(mainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(mainWindowHandle);
                }
                else
                {
                    MessageBox.Show("Gw2-64.exe is not running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error bringing Gw2-64.exe to the foreground: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void LoadConfigText(TextBox Runinfo, TextBox Squadinfo, TextBox Guild, TextBox Welcome, TextBox Symbols)
        {
            try
            {

                // Überprüfen, ob die Datei existiert
                if (File.Exists(FILE_PATH))
                {
                    // Den gesamten Text aus der Datei lesen
                    string configText = File.ReadAllText(FILE_PATH);

                    // Laden von Runinfo
                    LoadTextFromConfig("Runinfo:", Runinfo, configText, DEFAULT_RUN_INFO);

                    // Laden von Squadinfo
                    LoadTextFromConfig("Squadinfo:", Squadinfo, configText, DEFAULT_SQUAD_INFO);

                    // Laden von Guild
                    LoadTextFromConfig("Guild:", Guild, configText, DEFAULT_GUILD);

                    // Laden von Welcome
                    LoadTextFromConfig("Welcome:", Welcome, configText, DEFAULT_WELCOME);

                    // Laden von Symbols
                    LoadTextFromConfig("Symbols:", Symbols, configText, DEFAULT_SYMBOLS);
                }
                else
                {

                    Console.WriteLine($"Config file does not exist. Will try to create it");
                    try
                    {
                        var fileStream = File.Create(FILE_PATH);
                        fileStream.Close();
                        LoadConfigText(Runinfo, Squadinfo, Guild, Welcome, Symbols);
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception, but don't call ReadConfigFile recursively
                        Console.WriteLine($"Error creating config file: {ex.Message}");
                        throw; // Re-throw the exception to prevent infinite recursion
                    }
                    // // Die Konfigurationsdatei existiert nicht
                    // MessageBox.Show("Die Konfigurationsdatei 'config.txt' wurde nicht gefunden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Fehler beim Laden der Konfigurationsdatei
                MessageBox.Show($"Fehler beim Laden der Konfigurationsdatei: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            bossTimer.Dispose(); // Dispose of the BossTimer first
            base.OnFormClosing(e);
            Application.Exit();
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Owner.Show();
            Dispose();
        }


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
                mezTimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
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
                    DateTime currentTimeMez = TimeZoneInfo.ConvertTimeFromUtc(currentTimeUtc, mezTimeZone);

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
                        if (bossEvent.NextRunTime <= currentTimeMez)
                        {
                            // Treat as if it were a previous boss for the specified duration
                            var remainingTime = currentTimeMez - bossEvent.NextRunTime;
                            if (remainingTime.TotalMinutes <= 14.99)
                            {
                                var listViewItemExpired = new ListViewItem(new[] { bossEvent.BossName, "" });
                                listViewItemExpired.Font = new Font("Segoe UI", 10);
                                listViewItemExpired.ForeColor = PastBossFontColor;
                                tempUpcomingBosses.Add(listViewItemExpired);
                            }
                            continue;  // Skip adding this boss to the regular upcoming bosses list
                        }

                        // Adjust currentTimeMez to the next day if needed
                        if (bossEvent.NextRunTime <= currentTimeMez)
                        {
                            currentTimeMez = currentTimeMez.AddDays(1);
                        }

                        // Überprüfe, ob das BossEvent bereits verwendet wurde
                        if (usedBossEvents.Contains(bossEventKey))
                        {
                            // Wenn bereits verwendet, füge das BossEvent mit 24 Stunden Verzögerung hinzu
                            var delayedBossEvent = bossEvent.Clone(); // Annahme: BossEvent ist klonbar

                            // If the boss event is scheduled after midnight, adjust the timing to the next day
                            if (delayedBossEvent.NextRunTime.TimeOfDay < currentTimeMez.TimeOfDay)
                            {
                                delayedBossEvent.NextRunTime = delayedBossEvent.NextRunTime.AddDays(1);
                            }

                            // Generate a unique key for the delayed BossEvent
                            string delayedBossEventKey = $"{delayedBossEvent.BossName}_{delayedBossEvent.Category}_{delayedBossEvent.Timing}";

                            // Check if the delayed BossEvent has already been used
                            if (!usedBossEvents.Contains(delayedBossEventKey))
                            {
                                var remainingBossTime = delayedBossEvent.NextRunTime - currentTimeMez;
                                var formattedRemainingTime = $"{(int)remainingBossTime.TotalHours:D2}:{remainingBossTime.Minutes:D2}:{remainingBossTime.Seconds:D2}";

                                var listViewItemDelayed = new ListViewItem(new[] {
                                delayedBossEvent.BossName,
                                formattedRemainingTime
                            });

                                listViewItemDelayed.Font = new Font("Segoe UI", 10);
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
                            var remainingBossTime = bossEvent.NextRunTime - currentTimeMez;
                            var formattedRemainingTime = $"{(int)remainingBossTime.TotalHours:D2}:{remainingBossTime.Minutes:D2}:{remainingBossTime.Seconds:D2}";

                            var listViewItem = new ListViewItem(new[] {
                                bossEvent.BossName,
                                formattedRemainingTime
                            });

                            if (HasSameTimeAndCategory(upcomingBosses, bossEvent))
                            {
                                listViewItem.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                            }
                            else
                            {
                                listViewItem.Font = new Font("Segoe UI", 10);
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
                                var remainingBossTimeDelayed = delayedBossEvent.NextRunTime - currentTimeMez;
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
                            listViewItem.Font = new Font("Segoe UI", 10);
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


            private bool HasSameTimeAndCategory(List<BossEventRun> upcomingBosses, BossEventRun currentBossEvent)
            {
                return upcomingBosses.Any(bossEvent =>
                    bossEvent != currentBossEvent &&
                    bossEvent.NextRunTime == currentBossEvent.NextRunTime &&
                    bossEvent.Category == currentBossEvent.Category &&
                    bossEvent.BossName != currentBossEvent.BossName);
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
}
