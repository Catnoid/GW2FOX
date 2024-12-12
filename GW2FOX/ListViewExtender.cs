using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
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
                if (e.Item.Selected)
                {
                    e.Graphics.DrawString(e.SubItem.Text, font, Brushes.White, e.Bounds);
                }
                else
                {
                    DrawTextWithShadow(e.Graphics, e.SubItem.Text, font, e.Bounds, e.Item.ForeColor);
                }
            }
        }

        private void DrawTextWithShadow(Graphics g, string text, Font font, Rectangle bounds, Color textColor)
        {
            Point textLocation = new Point(bounds.Left, bounds.Top);

            TextRenderer.DrawText(g, text, font, new Point(textLocation.X - 1, textLocation.Y - 1), Color.Black);
            TextRenderer.DrawText(g, text, font, new Point(textLocation.X + 1, textLocation.Y - 1), Color.Black);
            TextRenderer.DrawText(g, text, font, new Point(textLocation.X - 1, textLocation.Y + 1), Color.Black);
            TextRenderer.DrawText(g, text, font, new Point(textLocation.X + 1, textLocation.Y + 1), Color.Black);

            TextRenderer.DrawText(g, text, font, textLocation, textColor, Color.Transparent, TextFormatFlags.Default);
        }
    }

    public class ListViewExtender : IDisposable
    {
        private readonly Dictionary<int, ListViewColumn> columns = new Dictionary<int, ListViewColumn>();
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

            var secondColumn = new ListViewTextColumn(1);
            AddColumn(secondColumn);

            var thirdColumn = new ListViewTextColumn(2);
            AddColumn(thirdColumn);

            this.ListView.OwnerDraw = true;
            this.ListView.DrawItem += OnDrawItem;
            this.ListView.DrawSubItem += OnDrawSubItem;
            this.ListView.DrawColumnHeader += OnDrawColumnHeader;
            this.ListView.MouseMove += OnMouseMove;
            this.ListView.MouseClick += OnMouseClick;
            this.ListView.MouseMove += OnMouseHover;

            this.Font = new Font(ListView.Font.FontFamily, ListView.Font.Size);
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

            return this.columns.TryGetValue(index, out column) ? column : null;
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
            this.Click?.Invoke(this, new ListViewColumnMouseEventArgs(e, item, subItem));
        }

        public virtual void MouseHover(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            this.Hover?.Invoke(this, new ListViewColumnMouseHoverEventArgs(e, item, subItem));
        }

        public virtual void Invalidate(ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            this.Extender?.ListView.Invalidate(subItem.Bounds);
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
                DrawImageButton(e.Graphics, e.Bounds, true);
                this.hot = e.Bounds;
            }
            else
            {
                DrawImageButton(e.Graphics, e.Bounds, false);
            }
        }

        private void DrawImageButton(Graphics g, Rectangle bounds, bool hot)
        {
            Image backgroundImage = Properties.Resources.Waypoint;

            Rectangle imageBounds = new Rectangle(bounds.Location, new Size(17, 17));

            if (hot)
            {
                imageBounds.Inflate(2, 2);
            }

            g.DrawImage(backgroundImage, imageBounds);
        }
    }
}
