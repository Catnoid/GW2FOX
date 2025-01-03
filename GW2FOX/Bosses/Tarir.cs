﻿using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GW2FOX
{
    public partial class Tarir : BaseForm
    {
        public Tarir()
        {
            InitializeComponent();
            LoadConfigText(Runinfo, Squadinfo, Guild, Welcome, Symbols);
            _ = LoadItemPriceInformation();
        }




        // Variable zur Speicherung des Ursprungs der Seite
        private string originPage;

        // Konstruktor, der den Ursprung der Seite als Parameter akzeptiert
        public Tarir(string origin) : this()
        {
            InitializeItemPriceTextBox();

            // Setze den Ursprung der Seite
            originPage = origin;
        }

        private void InitializeItemPriceTextBox()
        {
            // Remove this line since Itempriceexeofzhaitan is already declared in the designer.
            // Itempriceexeofzhaitan = new TextBox(); 
            TarirCost.Text = "Item-Preis: Wird geladen...";
            TarirCost.AutoSize = true;
            TarirCost.ReadOnly = true;
            TarirCost.Location = new Point(/* Specify the X and Y coordinates */);
            Controls.Add(TarirCost);
        }



        private async Task LoadItemPriceInformation()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://api.guildwars2.com/v2/items/76063";
                    string jsonResult = await client.GetStringAsync(apiUrl);

                    JObject resultObject = JObject.Parse(jsonResult);

                    string itemName = (string)resultObject["name"];
                    string chatLink = (string)resultObject["chat_link"];
                    int itemPriceCopper = await GetItemPriceCopper();

                    int gold = itemPriceCopper / 10000;
                    int silver = (itemPriceCopper % 10000) / 100;
                    int copper = itemPriceCopper % 100;

                    // Update the existing "Itempriceexeofzhaitan" TextBox text
                    TarirItemName.Text = $"{itemName}";

                    TarirCost.Text = $"{chatLink}, Price: {gold} Gold, {silver} Silver, {copper} Copper";
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
                    string jsonResult = await client.GetStringAsync("https://api.guildwars2.com/v2/commerce/prices/76063");
                    JObject resultObject = JObject.Parse(jsonResult);
                    return (int)resultObject["sells"]["unit_price"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Abrufen des Item-Preises: {ex.Message}");
            }
        }





        private void Beheinfo_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Beheinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Beheinstance_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Beheinstance.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
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
            Clipboard.SetText(Welcome.Text);

            BringGw2ToFront();
        }



        private void North_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Noth.Text);

            BringGw2ToFront();
        }

        private void East_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(East.Text);

            BringGw2ToFront();
        }

        private void West_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(West.Text);

            BringGw2ToFront();
        }

        private void Bombs_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Bombs.Text);

            BringGw2ToFront();
        }

        private void Spray_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Spray.Text);

            BringGw2ToFront();
        }

        private void Gliding_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Gliding.Text);

            BringGw2ToFront();
        }

        private void Shroom_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Shroom.Text);

            BringGw2ToFront();
        }

        private void Southside_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Southside.Text);

            BringGw2ToFront();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                string homepageUrl = "https://wiki.guildwars2.com/wiki/Battle_in_Tarir";
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

        private void button18_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TarirCost.Text);
            BringGw2ToFront();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Mapinfo.Text);

            BringGw2ToFront();
        }
    }
}