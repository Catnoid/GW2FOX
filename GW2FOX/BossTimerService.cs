﻿using System.Reflection;
using static GW2FOX.BossTimings;

namespace GW2FOX
{
    public static class Extensions
    {
        public static void DoubleBuffered(this ListView listView, bool enable)
        {
            if (SystemInformation.TerminalServerSession)
                return;

            listView.GetType().InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, listView, new object[] { enable });
        }

        public static void SetDoubleBuffered(this Control control)
        {
            if (SystemInformation.TerminalServerSession)
                return;

            control.GetType().InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, control, new object[] { true });
        }
    }


    public static class BossTimerService
    {
        private static readonly string TimeZoneId = "W. Europe Standard Time";
        private static readonly Color DefaultFontColor = Color.Blue;
        private static readonly Color PastBossFontColor = Color.OrangeRed;

        public static Overlay? _overlay { get; set; }
        public static BossTimer? _bossTimer { get; set; }
        private static ListView CustomBossList { get; set; } = new();

        private static ToolTip toolTip = new ToolTip();



        static BossTimerService()
        {
            Initialize();
        }

        public static void UpdateCustomBossList(ListView updatedList)
        {
            CustomBossList = updatedList;
        }

        private static void InitializeBossTimerAndOverlay()
        {
            _bossTimer = new BossTimer(CustomBossList);

            if (_overlay is { IsDisposed: false }) return;
            _overlay = new Overlay(CustomBossList);
            _overlay.WindowState = FormWindowState.Normal;
        }

        private static void InitializeCustomBossList()
        {
            CustomBossList = new ListView();

            // Aktivieren Sie das Double-Buffering für die ListView
            SetDoubleBuffered(CustomBossList);

            CustomBossList.View = View.Details;
            CustomBossList.Columns.Add("Boss Name");
            CustomBossList.Columns.Add("Time");
            CustomBossList.Location = new Point(0, 0);
            CustomBossList.ForeColor = Color.Black;
            CustomBossList.MouseClick += ListView_MouseClick;
            CustomBossList.MouseHover += ListView_MouseHover;
            CustomBossList.HeaderStyle = ColumnHeaderStyle.None;
        }


        public static void SetDoubleBuffered(Control control)
        {
            if (SystemInformation.TerminalServerSession)
                return;

            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, control, new object[] { true });
        }

        private static void MyMenuItem_Click(object sender, EventArgs e)
    {
        var menuItem = (ToolStripMenuItem)sender;
        var contextMenu = (ContextMenuStrip) menuItem.Owner!;
        var owner = contextMenu.SourceControl;
        var listView = (ListView)owner!;

        if (listView.SelectedItems.Count <= 0) return;
        var listViewItem = listView.SelectedItems[0];
        var bossEvent = (BossEventRun)listViewItem.Tag!;

        if (DoneBosses.ContainsKey(bossEvent.NextRunTime.Date))
        {
            DoneBosses[bossEvent.NextRunTime.Date].Add(bossEvent.BossName);
        }
        else
        {
            DoneBosses.Add(bossEvent.NextRunTime.Date, [bossEvent.BossName]);
        }
 
    }

    private static void ListView_MouseClick(object? sender, MouseEventArgs e)
    {
        
        var listView = sender as ListView;
        var selectedItem = listView?.GetItemAt(e.X, e.Y);
        if (listView == null) return;
        if (selectedItem is not { Tag: BossEventRun bossEvent }) return;
        if (e.Button == MouseButtons.Left)
        {
            // Assuming each ListViewItem.Tag holds the corresponding BossEventRun
            var textToCopy = bossEvent.Waypoint; // Use 'waypoint' property of BossEventRun instead.
            Clipboard.SetText(textToCopy);
            MessageBox.Show("Waypoint of " + bossEvent.BossName + " has been copied to clipbaord", "Waypoint Copied!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        } else if (e.Button == MouseButtons.Right)
        {
            var dialogResult = MessageBox.Show("Are You Sure you want to Uncheck \"" + bossEvent.BossName + "\"?", "Confirm Uncheck Boss \"" + bossEvent.BossName + "\"", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult != DialogResult.Yes) return;
            Worldbosses.RemoveBossNameFromConfig(bossEvent.BossName);
            MessageBox.Show($"Boss \"" + bossEvent.BossName + "\" has been Unchecked!", "\"" + bossEvent.BossName + "\" Unchecked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    
    private static void ListView_MouseHover(object? sender, EventArgs e)
    {
        ListView listView = sender as ListView;
        Point mousePosition = listView.PointToClient(Cursor.Position);
        ListViewItem hoveredItem = listView.GetItemAt(mousePosition.X, mousePosition.Y);
        if (hoveredItem is not { Tag: BossEvent bossEvent })
        {
            toolTip.Hide(listView);
            return;
        }
        if (!"".Equals(bossEvent.Waypoint))
        {
            // Show the tooltip
            toolTip.Show("Click to copy the Waypoint to clipboard", listView, mousePosition,
                1000); // tooltip disappears after 1 second (1000 milliseconds)
        }
        
    }





    public static void Timer_Click(object sender, EventArgs e)
    {
        
        Update();
    }


    private static void Initialize()
    {
        InitializeCustomBossList();
        if (_overlay == null || _overlay.IsDisposed)
        {
            InitializeBossTimerAndOverlay();
        }
    }

    private static void Update()
    {
        Initialize();
        
        _bossTimer?.Start();
        if (_overlay is { Visible: false })
        {
            _overlay.Show();
        }
    }


    public class BossTimer : IDisposable
        {
           
            private readonly ListView _bossList;
            private readonly TimeZoneInfo _mezTimeZone;
            private readonly System.Threading.Timer _timer;

            public BossTimer(ListView bossList)
            {
                this._bossList = bossList;
                _mezTimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
                _timer = new System.Threading.Timer(TimerCallback, null, 0, 1000);
            }
       

            public void Start()
            {
                _timer.Change(0, 1000);
            }

            public void Stop()
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }

        public static void UpdateCustomBossList(ListView updatedList)
        {
            CustomBossList = updatedList;
           
            BossTimer.UpdateCustomBossList(updatedList);
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
                if (!_bossList.IsHandleCreated) return;

                _bossList.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
                {
                    try
                    {
                        // Read the boss names from the configuration file
                        List<string> bossNamesFromConfig = BossList23;

                        var bossEventGroups = BossEventGroups
                            .Where(bossEventGroup => bossNamesFromConfig.Contains(bossEventGroup.BossName))
                            .ToList();

                        var upcomingBosses = bossEventGroups
                            .SelectMany(bossEventGroup => bossEventGroup.GetNextRuns())
                            .ToList();
                        
                        
                        var pastBosses =  bossEventGroups
                            .SelectMany(bossEventGroup => bossEventGroup.GetPreviousRuns())
                            .ToList();

                        // var allBosses = upcomingBosses.ToList();
                        var allBosses = upcomingBosses.Concat(pastBosses).ToList();

                        var listViewItems = new List<ListViewItem>();

                        // Use a HashSet to keep track of added boss names
                        HashSet<string> addedBossNames = new HashSet<string>();

                        allBosses.Sort((bossEvent1, bossEvent2) =>
                        {
                            // Compare the adjusted timings for the next day
                            int adjustedTimingComparison = bossEvent1.NextRunTime.CompareTo(bossEvent2.NextRunTime);
                            if (adjustedTimingComparison != 0) return adjustedTimingComparison;


                            // If durations and timings are equal, sort by categories (if necessary)
                            int categoryComparison = String.Compare(bossEvent1.Category, bossEvent2.Category, StringComparison.Ordinal);
                            if (categoryComparison != 0) return categoryComparison;

                            return 0;
                        });

                        foreach (var bossEvent in allBosses)
                        {
                            string bossNameKey = $"{bossEvent.BossName}_{bossEvent.NextRunTime}";

                            // Calculate the end time of the boss event based on the current time
                            DateTime timeToShow = bossEvent.NextRunTime;
                            if (bossEvent.IsPreviewBoss)
                            {
                                timeToShow = bossEvent.NextRunTimeEnding;
                            }

                            
                                // Calculate remaining time until the end of the boss event
                                TimeSpan remainingTime = timeToShow - GlobalVariables.CURRENT_DATE_TIME;

                                string remainingTimeFormat = $"{(int)remainingTime.TotalHours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";

                                Color fontColor = GetFontColor(bossEvent);

                                var listViewItem = new ListViewItem(new[] { bossEvent.BossName, remainingTimeFormat });
                                listViewItem.ForeColor = fontColor;
                                listViewItem.Tag = bossEvent;

                                // Neue Bedingung hinzufügen, um zu prüfen, ob ein Bossevent zur selben Zeit stattfindet wie ein anderes Bossevent derselben Kategorie
                                if (HasSameTimeAndCategory(allBosses, bossEvent))
                                {
                                    listViewItem.Font = new Font("Segoe UI", 10, FontStyle.Italic | FontStyle.Bold);
                                }
                                else
                                {
                                    listViewItem.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                                }

                                listViewItems.Add(listViewItem);
                            
                        }

                        UpdateListViewItems(listViewItems);
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex, "UpdateBossList");
                    }
                });
            }



            private bool HasSameTimeAndCategory(List<BossEventRun> allBosses, BossEventRun currentBossEvent)
            {
                return allBosses.Any(bossEvent =>
                    bossEvent != currentBossEvent &&
                    bossEvent.NextRunTime == currentBossEvent.NextRunTime &&
                    bossEvent.Category == currentBossEvent.Category &&
                    bossEvent.BossName != currentBossEvent.BossName);
            }



            private void UpdateListViewItems(List<ListViewItem> listViewItems)
            {
                _bossList.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate

                {
                    try
                    {
                        if (_bossList.IsHandleCreated)
                        {
                            _bossList.BeginUpdate();
                            _bossList.Items.Clear();
                            _bossList.Items.AddRange(listViewItems.ToArray());
                            _bossList.EndUpdate();
                        }
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex, "UpdateListViewItems");
                    }
                });
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
                


                if (bossEvent.IsPreviewBoss)
                {
                    return PastBossFontColor;
                }

                switch (bossEvent.Category)
                {
                    case "Maguuma":
                        return Color.LimeGreen;
                    case "Desert":
                        return Color.DeepPink;
                    case "WBs":
                        return Color.Black;
                    case "Ice":
                        return Color.DeepSkyBlue;
                    case "Cantha":
                        return Color.Blue;
                    case "SotO":
                        return Color.Orange;
                    case "LWS2":
                        return Color.LightYellow;
                    case "LWS3":
                        return Color.ForestGreen;
                    default:
                        return DefaultFontColor;
                }
            }


            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposing) return;
                // _bossTimer = null;
                _timer.Dispose();
            }

            ~BossTimer()
            {
                Dispose(false);
            }
        }
    }

}