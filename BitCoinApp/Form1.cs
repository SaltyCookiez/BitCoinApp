using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;

namespace BitCoinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnGetRates_Click(object sender, EventArgs e)
        {
            float rate;
            string code;
            if (CurrencyCombo.SelectedItem == null)
            {
                MessageBox.Show("Select a currency.");
                return;
            }

            if (string.IsNullOrEmpty(amountOfCoinBox.Text))
            {
                MessageBox.Show("Enter an amount.");
                return;
            }
            if (CurrencyCombo.SelectedItem.ToString() == "EUR")
            {
                resultLabel.Visible = true;
                ResultTextBox.Visible = true;
                btnGetRates.Visible = true;
                BitCoinRates bitcoin = GetRates();
                float result = Int32.Parse(amountOfCoinBox.Text) * bitcoin.bpi.EUR.rate_float;
                ResultTextBox.Text = $"{result.ToString()} {bitcoin.bpi.EUR.code}";
                rate = bitcoin.bpi.EUR.rate_float;
                code = bitcoin.bpi.EUR.code;
            }
            if (CurrencyCombo.SelectedItem.ToString() == "USD")
            {
                resultLabel.Visible = true;
                ResultTextBox.Visible = true;
                btnGetRates.Visible = true;
                BitCoinRates bitcoin = GetRates();
                float result = Int32.Parse(amountOfCoinBox.Text) * bitcoin.bpi.USD.rate_float;
                ResultTextBox.Text = $"{result.ToString()} {bitcoin.bpi.USD.code}";
                rate = bitcoin.bpi.EUR.rate_float;
                code = bitcoin.bpi.EUR.code;
            }
        }

        public static BitCoinRates GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitCoinRates bitcoin;
            using(var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoin = JsonConvert.DeserializeObject<BitCoinRates>(response);
            }

            return bitcoin;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CurrencyCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
