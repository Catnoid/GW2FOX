﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static GW2FOX.ListViewColumn;


namespace GW2FOX
{
    public enum ColumnType
    {
        Button,
        SecondColumn,
        ThirdColumn
    }

    public class ListViewTextColumn : ListViewColumn
    {
        public ListViewTextColumn(int columnIndex)
                : base(columnIndex)
            {
            }

         public override void Draw(DrawListViewSubItemEventArgs e)
         {
             if (e.Item == null) return;
             var itemFont = e.Item.Font;
             using (var font = itemFont)
                {
                    // Setze die Schriftart und Farbe basierend auf bestimmten Bedingungen
                    if (e.Item.Selected)
                    {
                        e.Graphics.DrawString(e.SubItem.Text, font, Brushes.White, e.Bounds);
                    }
                    else
                    {
                        // Definiere die Position des Zeittexts
                        Point timeTextLocation = new Point(e.Bounds.Left + 0, e.Bounds.Top + 0);


                        
                        {
                        TextRenderer.DrawText(e.Graphics, e.SubItem.Text, font, new Point(timeTextLocation.X - 1, timeTextLocation.Y - 1), Color.Black);
                        TextRenderer.DrawText(e.Graphics, e.SubItem.Text, font, new Point(timeTextLocation.X + 1, timeTextLocation.Y - 1), Color.Black);
                        TextRenderer.DrawText(e.Graphics, e.SubItem.Text, font, new Point(timeTextLocation.X - 1, timeTextLocation.Y + 1), Color.Black);
                        TextRenderer.DrawText(e.Graphics, e.SubItem.Text, font, new Point(timeTextLocation.X + 1, timeTextLocation.Y + 1), Color.Black);
                    }

                        // Zeichne den Zeittext ohne Umrandung (darüber, um die Umrandung zu überlagern)
                        TextRenderer.DrawText(e.Graphics, e.SubItem.Text, font, timeTextLocation, e.Item.ForeColor, Color.Transparent, TextFormatFlags.Default);
                    }
                }
            }
        }




public class ListViewExtender : IDisposable
    {
        private readonly Dictionary<int, ListViewColumn> columns = new Dictionary<int, ListViewColumn>();
        

        // Füge diese Dictionary hinzu, um die Breiten der Spalten festzulegen
        private readonly Dictionary<ColumnType, int> columnWidths = new Dictionary<ColumnType, int>();
    

        private bool disposed;

        public ListViewExtender(ListView listView)
        {
            if (listView == null)
                throw new ArgumentNullException("listView");

            if (listView.View != View.Details)
                throw new ArgumentException(null, "listView");

            this.ListView = listView;
            columnWidths[ColumnType.Button] = 50;
            columnWidths[ColumnType.SecondColumn] = 100;
            columnWidths[ColumnType.ThirdColumn] = 150;
            var buttonColumn = new ListViewButtonColumn(0);
            AddColumn(buttonColumn);

            // Erstelle die zweite Spalte
            var secondColumn = new ListViewTextColumn(1);
            AddColumn(secondColumn);

            // Erstelle die dritte Spalte
            var thirdColumn = new ListViewTextColumn(2);
            AddColumn(thirdColumn);

            this.ListView.OwnerDraw = true;
            this.ListView.DrawItem += OnDrawItem;
            this.ListView.DrawSubItem += OnDrawSubItem;
            this.ListView.DrawColumnHeader += OnDrawColumnHeader;
            this.ListView.MouseMove += OnMouseMove;
            this.ListView.MouseClick += OnMouseClick;
            this.ListView.MouseMove += OnMouseHover;
            // this.ListView.ItemMouseHover += OnMouseHover;

            this.Font = new Font(ListView.Font.FontFamily, ListView.Font.Size - 0);
        }
   

        public virtual Font Font { get; private set; }

        public ListView ListView { get; private set; }

        protected virtual void OnMouseHover(object sender, MouseEventArgs e)
        {
            ListViewItem item;
            ListViewItem.ListViewSubItem sub;
            ListViewColumn column = this.GetColumnAt(e.X, e.Y, out item, out sub);

            if (column != null)
            {
                column.MouseHover(e, item, sub);
            }
        }

        protected virtual void OnMouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem item;
            ListViewItem.ListViewSubItem sub;
            ListViewColumn column = this.GetColumnAt(e.X, e.Y, out item, out sub);

            if (column != null)
            {
                column.MouseClick(e, item, sub);
            }
        }

        public ListViewColumn GetColumnAt(int x, int y, out ListViewItem item, out ListViewItem.ListViewSubItem subItem)
        {
            subItem = null;
            item = ListView.GetItemAt(x, y);

            if (item == null)
                return null;

            subItem = item.GetSubItemAt(x, y);

            if (subItem == null)
                return null;

            for (int i = 0; i < item.SubItems.Count; i++)
            {
                if (item.SubItems[i] == subItem)
                {
                    return this.GetColumn(i);
                }
            }

            return null;
        }

        protected virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem item;
            ListViewItem.ListViewSubItem sub;
            ListViewColumn column = this.GetColumnAt(e.X, e.Y, out item, out sub);

            if (column != null)
            {
                column.Invalidate(item, sub);
                return;
            }

            if (item != null)
            {
                this.ListView.Invalidate(item.Bounds);
            }
        }

        protected virtual void OnDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;

        }

        protected virtual void OnDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            ListViewColumn column = this.GetColumn(e.ColumnIndex);

            if (column == null)
            {
                e.DrawDefault = true;
                return;
            }

            column.Draw(e);
        
        }



        protected virtual void OnDrawItem(object sender, DrawListViewItemEventArgs e)
        {
            
        }

      

        public void AddColumn(ListViewColumn column)
        {
            if (column == null)
                throw new ArgumentNullException("column");

            column.Extender = this;
            this.columns[column.ColumnIndex] = column;
        }

        public ListViewColumn GetColumn(int index)
        {
            ListViewColumn column;

            return this.columns.TryGetValue(index, out column)
                ? column
                : null;
        }

        public IEnumerable<ListViewColumn> Columns
        {
            get { return this.columns.Values; }
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                if (this.Font != null)
                {
                    this.Font.Dispose();
                    this.Font = null;
                }

                this.disposed = true;
            }
        }
    }

    public abstract class ListViewColumn
    {
        public event EventHandler<ListViewColumnMouseEventArgs> Click;
        public event EventHandler<ListViewColumnMouseHoverEventArgs> Hover;

        protected ListViewColumn(int columnIndex)
        {
            if (columnIndex < 0)
                throw new ArgumentException(null, "columnIndex");

            this.ColumnIndex = columnIndex;
        }

        public virtual ListViewExtender Extender { get; protected internal set; }

        public int ColumnIndex { get; private set; }

        public virtual Font Font
        {
            get { return this.Extender == null ? null : this.Extender.Font; }
        }

        public ListView ListView
        {
            get { return this.Extender == null ? null : this.Extender.ListView; }
        }

        public abstract void Draw(DrawListViewSubItemEventArgs e);

        public virtual void MouseClick(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            if (this.Click != null)
            {
                this.Click(this, new ListViewColumnMouseEventArgs(e, item, subItem));
            }
        }

        public virtual void MouseHover(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            if (this.Hover != null)
            {
                this.Hover(this, new ListViewColumnMouseHoverEventArgs(e, item, subItem));
            }
        }

        public virtual void Invalidate(ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            if (this.Extender != null)
            {
                this.Extender.ListView.Invalidate(subItem.Bounds);
            }
        }
    }

    public class ListViewColumnMouseEventArgs : MouseEventArgs
    {
        public ListViewColumnMouseEventArgs(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            this.Item = item;
            this.SubItem = subItem;
        }

        public ListViewItem Item { get; private set; }

        public ListViewItem.ListViewSubItem SubItem { get; private set; }
    }

    public class ListViewColumnMouseHoverEventArgs : ListViewItemMouseHoverEventArgs
    {
        public ListViewColumnMouseHoverEventArgs(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
            : base(item)
        {
            this.Item = item;
            this.SubItem = subItem;
        }

        public ListViewItem Item { get; private set; }

        public ListViewItem.ListViewSubItem SubItem { get; private set; }
    }

    public class ListViewButtonColumn : ListViewColumn
    {
        private Rectangle hot = Rectangle.Empty;

        public ListViewButtonColumn(int columnIndex)
            : base(columnIndex)
        {
        }

        public bool FixedWidth { get; set; }

        public bool DrawIfEmpty { get; set; }

        public override ListViewExtender Extender
        {
            get { return base.Extender; }

            protected internal set
            {
                base.Extender = value;

                if (this.FixedWidth)
                {
                    base.Extender.ListView.ColumnWidthChanging += this.OnColumnWidthChanging;
                }
            }
        }

        protected virtual void OnColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == ColumnIndex)
            {
                e.Cancel = true;
                e.NewWidth = this.ListView.Columns[e.ColumnIndex].Width;
            }
        }

        public override void Draw(DrawListViewSubItemEventArgs e)
        {
            if (this.hot != Rectangle.Empty)
            {
                if (this.hot != e.Bounds)
                {
                    this.ListView.Invalidate(this.hot);
                    this.hot = Rectangle.Empty;
                }
            }

            if ((!this.DrawIfEmpty) && (string.IsNullOrEmpty(e.SubItem.Text)))
                return;

            Point mouse = e.Item.ListView.PointToClient(Control.MousePosition);

            if ((this.ListView.GetItemAt(mouse.X, mouse.Y) == e.Item) &&
                (e.Item.GetSubItemAt(mouse.X, mouse.Y) == e.SubItem))
            {
                // Verwende die eigene Methode für den Button mit Hintergrundbild (ohne Text)
                DrawImageButton(e.Graphics, e.Bounds, true);
                this.hot = e.Bounds;
            }
            else
            {
                // Verwende die eigene Methode für den Button mit Hintergrundbild (ohne Text)
                DrawImageButton(e.Graphics, e.Bounds, false);
            }

            
        }

        private void DrawImageButton(Graphics g, Rectangle bounds, bool hot)
        {
            // Das Bild aus den Ressourcen laden
            Image backgroundImage = Properties.Resources.Waypoint;

            // Die Größe auf 17x17 Pixel setzen
            Rectangle imageBounds = new Rectangle(bounds.Location, new Size(17, 17));

            // Falls "hot" aktiviert ist, das Bild um 3 Pixel vergrößern
            if (hot)
            {
                imageBounds.Inflate(2, 2);
            }

            // Hintergrund zeichnen
            g.DrawImage(backgroundImage, imageBounds);

            // Falls "hot" aktiviert ist, Text anzeigen
            if (hot)
            {
                

            }
        }

    }
}