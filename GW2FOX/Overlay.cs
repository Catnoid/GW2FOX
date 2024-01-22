using static GW2FOX.BossTimings;

namespace GW2FOX
{
    public partial class Overlay : Form
    {
        public static ListView CustomBossList { get; private set; }
        private static readonly Color DefaultFontColor = Color.White;
        private static readonly Color PastBossFontColor = Color.OrangeRed;
        private static readonly Color MyAlmostBlackColor = Color.FromArgb(255, 1, 1, 1);

        private Point mouse_offset;


        public Overlay(ListView listViewItems)
        {
            InitializeComponent();
            if (Owner is BaseForm baseForm)
            {
                BossTimerService.UpdateCustomBossList(listViewItems);
            }
            CustomBossList = listViewItems;

            ListView overlayListView = CustomBossList;
            overlayListView.ForeColor = Color.Black;
            overlayListView.DrawItem += OverlayListView_DrawItem;

            // Konfiguriere das Overlay-Formular
            BackColor = MyAlmostBlackColor;
            TransparencyKey = MyAlmostBlackColor;
            TopMost = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Opacity = 1;
            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            Width = 230;
            Height = 310;
            AutoScroll = true;

            Panel listViewPanel = new Panel();
            listViewPanel.BackColor = Color.Transparent;

            // Berechne die Größe des listViewPanel
            int panelWidth = (int)(Width);
            int panelHeight = (int)(Height * 10);
            listViewPanel.Size = new Size(panelWidth, panelHeight);

            listViewPanel.Location = new Point(0, 0);

            // Erstelle die ListView
            overlayListView.ForeColor = Color.White;
            overlayListView.Font = new Font("Segoe UI", 10);
            overlayListView.BackColor = BackColor;

            // Setze die View auf Details, um die horizontale Scrollleiste zu deaktivieren
            overlayListView.View = View.Details;

            // Entferne die Spaltenüberschriften, um die horizontale Scrollleiste zu verbergen
            overlayListView.HeaderStyle = ColumnHeaderStyle.None;

            overlayListView.OwnerDraw = true;
            overlayListView.Location = new Point(0, 0);
            overlayListView.Width = listViewPanel.Width;

            // Enable vertical scrollbar
            overlayListView.Scrollable = true;

            // Setze die Höhe unter Berücksichtigung der horizontalen Scrollleiste
            overlayListView.Height = listViewPanel.Height - SystemInformation.HorizontalScrollBarHeight;

            overlayListView.Enabled = true;
            overlayListView.ItemSelectionChanged += (sender, e) =>
            {
                overlayListView.SelectedIndices.Clear();
            };

            // Füge die ListView zum ListView Panel hinzu
            listViewPanel.Controls.Add(overlayListView);
            Controls.Add(listViewPanel);
        }



        private void OverlayListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            // Zeichne den Hintergrund
            e.DrawBackground();

            // Holen Sie sich das BossEventRun-Objekt aus dem ListViewItem.Tag
            if (e.Item.Tag is BossEventRun bossEvent)
            {
                // Überprüfen, ob das BossEvent zu den PreviewBosses gehört
                if (bossEvent.IsPreviewBoss)
                {
                    e.Item.ForeColor = PastBossFontColor; // Setzen Sie die Farbe auf OrangeRed für PreviewBosses
                }
                else
                {
                    // Setzen Sie die Farbe basierend auf der Kategorie des BossEvents
                    switch (bossEvent.Category)
                    {
                        case "Maguuma":
                            e.Item.ForeColor = Color.LimeGreen;
                            break;
                        case "Desert":
                            e.Item.ForeColor = Color.DeepPink;
                            break;
                        case "WBs":
                            e.Item.ForeColor = Color.WhiteSmoke;
                            break;
                        case "Ice":
                            e.Item.ForeColor = Color.DeepSkyBlue;
                            break;
                        case "Cantha":
                            e.Item.ForeColor = Color.Blue;
                            break;
                        case "SotO":
                            e.Item.ForeColor = Color.Yellow;
                            break;
                        case "LWS2":
                            e.Item.ForeColor = Color.LightYellow;
                            break;
                        case "LWS3":
                            e.Item.ForeColor = Color.ForestGreen;
                            break;
                        default:
                            e.Item.ForeColor = DefaultFontColor;
                            break;
                    }
                }
            }

            // Definiere den Text und die Schriftart
            string mainText = e.Item.Text;
            string timeText = e.Item.SubItems[1].Text;

            Font font = e.Item.Font;

            Point mainTextLocation = new Point(e.Bounds.Left + 2, e.Bounds.Top + 5);

            // Zeichne den Haupttext mit grauer Umrandung
            TextRenderer.DrawText(e.Graphics, mainText, font, new Point(mainTextLocation.X - 1, mainTextLocation.Y - 1), Color.Black);
            TextRenderer.DrawText(e.Graphics, mainText, font, new Point(mainTextLocation.X + 1, mainTextLocation.Y - 1), Color.Black);
            TextRenderer.DrawText(e.Graphics, mainText, font, new Point(mainTextLocation.X - 1, mainTextLocation.Y + 1), Color.Black);
            TextRenderer.DrawText(e.Graphics, mainText, font, new Point(mainTextLocation.X + 1, mainTextLocation.Y + 1), Color.Black);

            // Zeichne den Haupttext ohne Umrandung (darüber, um die Umrandung zu überlagern)
            TextRenderer.DrawText(e.Graphics, mainText, font, mainTextLocation, e.Item.ForeColor, Color.Transparent, TextFormatFlags.Default);

            // Definiere die Position des Zeittexts mit grauer Umrandung
            Point timeTextLocation = new Point(e.Bounds.Left + 145, e.Bounds.Top + 5); 

            // Zeichne den Zeittext mit grauer Umrandung
            TextRenderer.DrawText(e.Graphics, timeText, font, new Point(timeTextLocation.X - 1, timeTextLocation.Y - 1), Color.Black);
            TextRenderer.DrawText(e.Graphics, timeText, font, new Point(timeTextLocation.X + 1, timeTextLocation.Y - 1), Color.Black);
            TextRenderer.DrawText(e.Graphics, timeText, font, new Point(timeTextLocation.X - 1, timeTextLocation.Y + 1), Color.Black);
            TextRenderer.DrawText(e.Graphics, timeText, font, new Point(timeTextLocation.X + 1, timeTextLocation.Y + 1), Color.Black);

            // Zeichne den Zeittext ohne Umrandung (darüber, um die Umrandung zu überlagern)
            TextRenderer.DrawText(e.Graphics, timeText, font, timeTextLocation, e.Item.ForeColor, Color.Transparent, TextFormatFlags.Default);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                Location = mousePos;
            }
        }

        public void CloseOverlay()
        {
            Dispose();
        }
    }
}