﻿using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GW2FOX
{
    public partial class Ogrewars : BaseForm
    {
        private const string ItemApiUrl = "https://api.guildwars2.com/v2/items/46467";

        public Ogrewars()
        {
            InitializeComponent();
            LoadConfigText(Runinfo, Squadinfo, Guild, Welcome, Symbols);
            InitializeItemPriceTextBox();
            _ = LoadItemPriceInformation();
        }

        // Variable zur Speicherung des Ursprungs der Seite
        private string originPage;

        // Konstruktor, der den Ursprung der Seite als Parameter akzeptiert
        public Ogrewars(string origin) : this()
        {

            // Setze den Ursprung der Seite
            originPage = origin;
        }

        private void InitializeItemPriceTextBox()
        {
            Itemprice.Text = "Item-Preis: Wird geladen...";
            Itemprice.AutoSize = true;
            Itemprice.ReadOnly = true;
        }

        private async Task LoadItemPriceInformation()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Send a request to the API and get the response as JSON
                    string jsonResult = await client.GetStringAsync(ItemApiUrl);

                    // Convert the JSON to a JObject
                    JObject resultObject = JObject.Parse(jsonResult);

                    // Extract the item name
                    string itemName = (string)resultObject["name"];
                    string chatLink = (string)resultObject["chat_link"];

                    // Get the item price from a separate API call
                    int itemPriceCopper = await GetItemPriceCopper();

                    // Convert the item price to gold, silver, and copper
                    int gold = itemPriceCopper / 10000;
                    int silver = (itemPriceCopper % 10000) / 100;
                    int copper = itemPriceCopper % 100;

                    // Display the item name and price in the existing TextBox
                    Itemprice.Text = $"{chatLink}, Price: {gold} Gold, {silver} Silver, {copper} Copper";

                    Samname.Text = $"{itemName}";
                }
            }
            catch (Exception ex)
            {
                // Handle possible exceptions (e.g., if the API is not reachable)
                MessageBox.Show($"Oh NO something went wrong: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<int> GetItemPriceCopper()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Send a request to the API and get the response as JSON
                    string jsonResult = await client.GetStringAsync($"https://api.guildwars2.com/v2/commerce/prices/46467");

                    // Convert the JSON to a JObject
                    JObject resultObject = JObject.Parse(jsonResult);

                    // Extract the item price in copper
                    return (int)resultObject["sells"]["unit_price"];
                }
            }
            catch (Exception ex)
            {
                // Handle possible exceptions (e.g., if the API is not reachable)
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

        private void Ogrewarsinfo_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Ogrewarsinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Ogrewarsinstance_Click(object sender, EventArgs e)
        {
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Ogrewarsinstance.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }


        private void Itemprice_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Itemprice.Text);
            BringGw2ToFront();
        }

        private void Attentionogre_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Attentionogrewars.Text);
            BringGw2ToFront();
        }

        private void Preis_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Itemprice.Text);
            BringGw2ToFront();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string homepageUrl = "https://wiki.guildwars2.com/wiki/Ogre_Wars";
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
    }
}
