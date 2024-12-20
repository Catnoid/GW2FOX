﻿using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GW2FOX
{
    public partial class Drakkar : BaseForm
    {
        public Drakkar()
        {
            InitializeComponent();
            LoadConfigText(Runinfo, Squadinfo, Guild, Welcome, Symbols);
            _ = LoadItemPriceInformation();
        }



        // Variable zur Speicherung des Ursprungs der Seite
        private string originPage;

        // Konstruktor, der den Ursprung der Seite als Parameter akzeptiert
        public Drakkar(string origin) : this()
        {
            InitializeItemPriceTextBox();

            // Setze den Ursprung der Seite
            originPage = origin;
        }

        private void InitializeItemPriceTextBox()
        {
            // Remove this line since Itempriceexeofzhaitan is already declared in the designer.
            // Itempriceexeofzhaitan = new TextBox(); 
            DrakkarCost.Text = "Item-Preis: Wird geladen...";
            DrakkarCost.AutoSize = true;
            DrakkarCost.ReadOnly = true;
            DrakkarCost.Location = new Point(/* Specify the X and Y coordinates */);
            Controls.Add(DrakkarCost);
        }



        private async Task LoadItemPriceInformation()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://api.guildwars2.com/v2/items?ids=79959&lang=en";
                    string jsonResult = await client.GetStringAsync(apiUrl);

                    JArray resultArray = JArray.Parse(jsonResult);
                    JObject itemObject = (JObject)resultArray.First;

                    string itemName = (string)itemObject["name"];
                    string chatLink = (string)itemObject["chat_link"];
                    int itemPriceCopper = await GetItemPriceCopper();

                    int gold = itemPriceCopper / 10000;
                    int silver = (itemPriceCopper % 10000) / 100;
                    int copper = itemPriceCopper % 100;

                    // Update the existing "Itempriceexeofzhaitan" TextBox text
                    DrakkarItemName.Text = $"{itemName}";

                    DrakkarCost.Text = $"{chatLink}, Price: {gold} Gold, {silver} Silver, {copper} Copper";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oh NO something went wrong: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<int> GetItemPriceCopper()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonResult = await client.GetStringAsync("https://api.guildwars2.com/v2/commerce/prices/79959");
                    JObject resultObject = JObject.Parse(jsonResult);
                    return (int)resultObject["sells"]["unit_price"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Abrufen des Item-Preises: {ex.Message}");
            }
        }












        private void Runinfo_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Runinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Squadinfo_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Squadinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Guild_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Guild.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Welcome_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Welcome.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void DrakkarinfoClick(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Drakkarinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void DrakkarinstanceClick(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Drakkarinstance.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Attentiondrakkar_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Attentiondrakkar.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Drakkarportals_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Drakkarportals.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string homepageUrl = "https://wiki.guildwars2.com/wiki/Champion_of_the_Ice_Dragon";
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = homepageUrl,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim ?ffnen der Homepage: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(DrakkarCost.Text);
            BringGw2ToFront();
        }
    }
}
