﻿using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GW2FOX
{
    public partial class DredgeCommissar : BaseForm
    {
        private TextBox DredgeCommissarCoste;
        public DredgeCommissar()
        {
            InitializeComponent();
            LoadConfigText(Runinfo, Squadinfo, Guild, Welcome, Symbols);
            _ = LoadItemPriceInformation();
        }



        // Variable zur Speicherung des Ursprungs der Seite
        private string originPage;

        // Konstruktor, der den Ursprung der Seite als Parameter akzeptiert
        public DredgeCommissar(string origin) : this()
        {
            InitializeItemPriceTextBox();

            // Setze den Ursprung der Seite
            originPage = origin;
        }

        private void InitializeItemPriceTextBox()
        {
            // Remove this line since Itempriceexeofzhaitan is already declared in the designer.
            // Itempriceexeofzhaitan = new TextBox(); 
            DredgeCommissarCoste.Text = "Item-Preis: Wird geladen...";
            DredgeCommissarCoste.AutoSize = true;
            DredgeCommissarCoste.ReadOnly = true;
            DredgeCommissarCoste.Location = new Point(/* Specify the X and Y coordinates */);
            Controls.Add(DredgeCommissarCoste);
            DredgeCommissarCoste2.Text = "Item-Preis: Wird geladen...";
            DredgeCommissarCoste2.AutoSize = true;
            DredgeCommissarCoste2.ReadOnly = true;
            DredgeCommissarCoste2.Location = new Point(/* Specify the X and Y coordinates */);
            Controls.Add(DredgeCommissarCoste2);
        }



        private async Task LoadItemPriceInformation()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://api.guildwars2.com/v2/items?ids=39468&lang=en"; // Neue API-Adresse
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
                    DredgeCommissarItemName.Text = $"{itemName}";

                    DredgeCommissarCoste.Text = $"{chatLink}, Price: {gold} Gold, {silver} Silver, {copper} Copper";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oh NO something went wrong: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://api.guildwars2.com/v2/items?ids=19364&lang=en"; // Neue API-Adresse
                    string jsonResult = await client.GetStringAsync(apiUrl);

                    JArray resultArray = JArray.Parse(jsonResult);
                    JObject itemObject = (JObject)resultArray.First;

                    string itemName = (string)itemObject["name"];
                    string chatLink = "[&ClgSAAA=] - Call of the Commissar";
                    int itemPriceCopper = await GetItemPriceCopper2();

                    int gold = itemPriceCopper / 10000;
                    int silver = (itemPriceCopper % 10000) / 100;
                    int copper = itemPriceCopper % 100;

                    // Update the existing "Itempriceexeofzhaitan" TextBox text
                    DredgeCommissarItemName2.Text = $"{itemName}";

                    DredgeCommissarCoste2.Text = $"{chatLink}, Price: {gold} Gold, {silver} Silver, {copper} Copper";
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
                    string jsonResult = await client.GetStringAsync("https://api.guildwars2.com/v2/commerce/prices/39468"); // Neue API-Adresse
                    JObject resultObject = JObject.Parse(jsonResult);
                    return (int)resultObject["sells"]["unit_price"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Abrufen des Item-Preises: {ex.Message}");
            }
        }

        private async Task<int> GetItemPriceCopper2()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonResult = await client.GetStringAsync("https://api.guildwars2.com/v2/commerce/prices/19364"); // Neue API-Adresse
                    JObject resultObject = JObject.Parse(jsonResult);
                    return (int)resultObject["sells"]["unit_price"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Fehler beim Abrufen des Item-Preises: {ex.Message}");
            }
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
            // Copy the text from Leyline60 TextBox to the clipboard
            Clipboard.SetText(Welcome.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }



        private void Rata_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Rata.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        

        private void Beheinfo_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Beheinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Mapinfo_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Mapinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                string homepageUrl = "https://wiki.guildwars2.com/wiki/Defeat_the_dredge_commissar_(Dredgehaunt_Cliffs)";
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

            string combinedText = $"{DredgeCommissarCoste.Text}, {DredgeCommissarCoste2.Text}";
            Clipboard.SetText(combinedText);
            BringGw2ToFront();
            
        }

        private void ChakItemName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}