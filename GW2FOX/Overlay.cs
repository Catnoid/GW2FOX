using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static GW2FOX.BossTimings;
using System.Linq;
using System.Data.Common;
using static GW2FOX.ListViewColumn;

namespace GW2FOX
{
    public partial class Overlay : Form
    {
      
        private static readonly Color MyAlmostBlackColor = Color.FromArgb(255, 1, 1, 1);
        
        private ListViewExtender extender;

        public Overlay(ListView listViewItems)
        {
            InitializeComponent();
            // Konfiguriere das Overlay-Formular
            BackColor = MyAlmostBlackColor;
            TransparencyKey = MyAlmostBlackColor;
            TopMost = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Opacity = 1;
            Width = 240;
            Height = 316;
            AutoScroll = true;


            listView1.FullRowSelect = true;
            ListViewExtender extender = new ListViewExtender(listView1);
            // extend 2nd column
            ListViewButtonColumn buttonAction = new ListViewButtonColumn(1);
          
            buttonAction.FixedWidth = true;
            extender.AddColumn(buttonAction);
            for (int i = 0; i < 1; i++)
            {
                ListViewItem item = listView1.Items.Add("item" + i);
                item.SubItems.Add("button " + i);
            }
        }
        private void OnButtonActionClick(object sender, ListViewColumnMouseEventArgs e)
        {
            MessageBox.Show(this, @"you clicked " + e.SubItem.Text);
        }
    }
}