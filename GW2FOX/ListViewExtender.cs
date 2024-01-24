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
    public class ListViewExtender : IDisposable
    {
        private readonly Dictionary<int, ListViewColumn> _columns = new Dictionary<int, ListViewColumn>();
        private ListView CustomBossList;


        public ListViewExtender(ListView customBossList)
        {
            if (customBossList == null)
                throw new ArgumentNullException("customBossList");
            if (customBossList.View != View.Details)
                throw new ArgumentException(null, "customBossList");

            CustomBossList = customBossList;
            CustomBossList.OwnerDraw = true;
            CustomBossList.DrawItem += OnDrawItem;
            CustomBossList.DrawSubItem += OnDrawSubItem;
            CustomBossList.DrawColumnHeader += OnDrawColumnHeader;
            CustomBossList.MouseMove += OnMouseMove;
            CustomBossList.MouseClick += OnMouseClick;
            Font = new Font(CustomBossList.Font.FontFamily, CustomBossList.Font.Size - 2);
        }

        public virtual Font Font { get; private set; }
        public ListView ListView { get { return CustomBossList; } }

        public virtual void OnMouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem clickedItem;
            ListViewItem.ListViewSubItem clickedSubItem;
            ListViewColumn column = GetColumnAt(e.X, e.Y, out clickedItem, out clickedSubItem);
            if (column != null)
            {
                column.MouseClick(e, clickedItem, clickedSubItem);
            }
        }


        public ListViewColumn GetColumnAt(int x, int y, out ListViewItem item, out ListViewItem.ListViewSubItem subItem)
    {
        subItem = null;
        item = CustomBossList.GetItemAt(x, y);
        if (item == null)
            return null;

        subItem = item.GetSubItemAt(x, y);
        if (subItem == null)
            return null;

        for (int i = 0; i < item.SubItems.Count; i++)
        {
            if (item.SubItems[i] == subItem)
                return GetColumn(i);
        }

        return null;
    }

    protected virtual void OnMouseMove(object sender, MouseEventArgs e)
    {
        ListViewItem item;
        ListViewItem.ListViewSubItem sub;
        ListViewColumn column = GetColumnAt(e.X, e.Y, out item, out sub);
        if (column != null)
        {
            column.Invalidate(item, sub);
            return;
        }
        if (item != null)
        {
            ListView.Invalidate(item.Bounds);
        }
    }

    protected virtual void OnDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
    {
            Debug.WriteLine("Drawing ColumnHeader");
            e.DrawDefault = true;
    }

        protected virtual void OnDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Debug.WriteLine("Drawing SubItem");
            ListViewColumn column = GetColumn(e.ColumnIndex);
            if (column == null)
            {
                e.DrawDefault = true;
                return;
            }
            column.Draw(e);
        }


        protected virtual void OnDrawItem(object sender, DrawListViewItemEventArgs e)
    {
            Debug.WriteLine("Drawing Item");
        }

    public void AddColumns(ListViewColumn[] columns)
    {
        if (columns == null)
            throw new ArgumentNullException("columns");

        foreach (var column in columns)
        {
            AddColumn(column);
        }
    }

    public void AddColumn(ListViewColumn column)
    {
        if (column == null)
            throw new ArgumentNullException("column");
        column.Extender = this;
        _columns[column.ColumnIndex] = column;
    }

    public ListViewColumn GetColumn(int index)
    {
        if (_columns.TryGetValue(index, out var column))
        {
            return column;
        }
        return null;
    }

    public IEnumerable<ListViewColumn> Columns
    {
        get
        {
            return _columns.Values;
        }
    }

    public virtual void Dispose()
    {
        if (Font != null)
        {
            Font.Dispose();
            Font = null;
        }
    }
}

public abstract class ListViewColumn
    {
        public event EventHandler Click;

        public virtual void Invalidate(ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            if (Extender != null)
            {
                if (subItem != null)
                {
                    // Invalide mache nur das SubItem
                    Extender.ListView.Invalidate(subItem.Bounds);
                }
                else if (item != null)
                {
                    // Invalide mache die gesamte Zeile (alle SubItems)
                    Extender.ListView.Invalidate(item.Bounds);
                }
            }
        }

        public abstract ColumnHeader ColumnHeader { get; }
        protected ListViewColumn(int columnIndex)
        {
            if (columnIndex < 0)
                throw new ArgumentException(null, "columnIndex");
            ColumnIndex = columnIndex;
        }
        public virtual ListViewExtender Extender { get; protected internal set; }
        public int ColumnIndex { get; private set; }
        public virtual Font Font
        {
            get
            {
                return Extender == null ? null : Extender.Font;
            }
        }
        public ListView ListView
        {
            get
            {
                return Extender == null ? null : Extender.ListView;
            }
        }
        public abstract void Draw(DrawListViewSubItemEventArgs e);


        public virtual void MouseClick(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
        {
            if (Click != null)
            {
                Click(this, new ListViewColumnMouseEventArgs(e, item, subItem));
            }
        }

        public class ListViewColumnMouseEventArgs : MouseEventArgs
        {
            public ListViewColumnMouseEventArgs(MouseEventArgs e, ListViewItem item, ListViewItem.ListViewSubItem subItem)
                : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
            {
                Item = item;
                SubItem = subItem;
            }

            public ListViewItem Item { get; private set; }
            public ListViewItem.ListViewSubItem SubItem { get; private set; }
        }

    }

    public class ListViewButtonColumn : ListViewColumn
    {
        public string Text { get; set; }
        private Rectangle _hot = Rectangle.Empty;

        public ListViewButtonColumn(int columnIndex) : base(columnIndex)
        {
        }

        public bool FixedWidth { get; set; }
        public bool DrawIfEmpty { get; set; }

        public override ColumnHeader ColumnHeader
        {
            get
            {
                return new ColumnHeader { Text = "ButtonColumn", Width = 100 };
            }
        }

        public override ListViewExtender Extender
        {
            get { return base.Extender; }
            protected internal set
            {
                base.Extender = value;
                if (FixedWidth)
                {
                    base.Extender.ListView.ColumnWidthChanging += OnColumnWidthChanging;
                }
            }
        }

        protected virtual void OnColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == ColumnIndex)
            {
                e.Cancel = true;
                e.NewWidth = ListView.Columns[e.ColumnIndex].Width;
            }
        }

        public override void Draw(DrawListViewSubItemEventArgs e)
        {
            if (_hot != Rectangle.Empty)
            {
                if (_hot != e.Bounds)
                {
                    ListView.Invalidate(_hot);
                    _hot = Rectangle.Empty;
                }
            }
            if ((!DrawIfEmpty) && (string.IsNullOrEmpty(e.SubItem.Text)))
                return;
            Point mouse = e.Item.ListView.PointToClient(Control.MousePosition);
            if ((ListView.GetItemAt(mouse.X, mouse.Y) == e.Item) && (e.Item.GetSubItemAt(mouse.X, mouse.Y) == e.SubItem))
            {
                ButtonRenderer.DrawButton(e.Graphics, e.Bounds, e.SubItem.Text, Font, true, PushButtonState.Hot);
                _hot = e.Bounds;
            }
            else
            {
                ButtonRenderer.DrawButton(e.Graphics, e.Bounds, e.SubItem.Text, Font, false, PushButtonState.Default);
            }
        }
    }




}


