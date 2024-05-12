using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GW2FOX
{
    public partial class Fireshaman : BaseForm
    {
        public Fireshaman()
        {
            InitializeComponent();
            LoadConfigText(Runinfo, Squadinfo, Guild, Welcome, Symbols);
            _ = LoadItemPriceInformation();
        }




        private void InitializeItemPriceTextBox()
        {
            // Remove this line since Itempriceexeofzhaitan is already declared in the designer.
            // Itempriceexeofzhaitan = new TextBox(); 
            FireShamanCost.AutoSize = true;
            FireShamanCost.ReadOnly = true;
            FireShamanCost.Location = new Point(/* Specify the X and Y coordinates */);
            Controls.Add(FireShamanCost);
            FireShaman2Cost.AutoSize = true;
            FireShaman2Cost.ReadOnly = true;
            FireShaman2Cost.Location = new Point(/* Specify the X and Y coordinates */);
            Controls.Add(FireShaman2Cost);
        }



        private async Task LoadItemPriceInformation()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://api.guildwars2.com/v2/items/19367";
                    string jsonResult = await client.GetStringAsync(apiUrl);

                    JObject resultObject = JObject.Parse(jsonResult);

                    string itemName = (string)resultObject["name"];
                    string chatLink = "[&CggUAAA=]";
                    int itemPriceCopper = await GetItemPriceCopper();

                    int gold = itemPriceCopper / 10000;
                    int silver = (itemPriceCopper % 10000) / 100;
                    int copper = itemPriceCopper % 100;



                    FireShamanitemname.Text = $"{itemName}";
                    // Set FireShamanCost to display item name and price
                    FireShamanCost.Text = $"{chatLink}, Price: {gold} Gold, {silver} Silver, {copper} Copper";
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
                    string apiUrl = "https://api.guildwars2.com/v2/items/19365";
                    string jsonResult = await client.GetStringAsync(apiUrl);

                    JObject resultObject = JObject.Parse(jsonResult);

                    string itemName = (string)resultObject["name"];
                    string chatLink = "[&CgYUAAA=]";
                    int itemPriceCopper = await GetItemPriceCopper2();

                    int gold = itemPriceCopper / 10000;
                    int silver = (itemPriceCopper % 10000) / 100;
                    int copper = itemPriceCopper % 100;


                    FireShaman2itemname.Text = $"{itemName}";
                    // Set FireShaman2Cost to display item name and price
                    FireShaman2Cost.Text = $"{chatLink}, Price: {gold} Gold, {silver} Silver, {copper} Copper";
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
                    string jsonResult = await client.GetStringAsync("https://api.guildwars2.com/v2/commerce/prices/19367");
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
                    string jsonResult = await client.GetStringAsync("https://api.guildwars2.com/v2/commerce/prices/19365");
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
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Runinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Squadinfo_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Squadinfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Guild_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Guild.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Welcome_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Welcome.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Fireshamaninfo_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Fireshamaninfo.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Fireshamaninstance_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Fireshamaninstance.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void AttentionFireshaman_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(AttentionFireshaman.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void Fireshamanrounds_Click(object sender, EventArgs e)
        {
            // Copy the text from Mawinfo TextBox to the clipboard
            Clipboard.SetText(Fireshamanrounds.Text);

            // Bring the Gw2-64.exe window to the foreground
            BringGw2ToFront();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string homepageUrl = "https://wiki.guildwars2.com/wiki/Defeat_the_fire_shaman_and_his_minions";
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
            string combinedText = $"{FireShamanCost.Text}, {FireShaman2Cost.Text}";
            Clipboard.SetText(combinedText);
            BringGw2ToFront();
        }
    }
}