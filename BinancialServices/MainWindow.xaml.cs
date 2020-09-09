using BinanceExchange.API.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using log4net;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace BinancialServices
{
    
    public class Details
    {

        /// <summary>
        ///  Repeat is (in seconds) how long to wait before each repetition of the code block, i.e. 
        ///  (5) would mean the program checks the price information every 5 seconds. 0.5 second as default.
        ///  Symbol refers to the currency pair you wish to check against. More information found on BinanceShell.cs.
        ///  PercentChange refers to the target percent change (can be minus, and is optional) that if reached 
        ///  will populate the TextBox with that figure. More information on BinanceShell.cs.
        /// </summary>

        public double Repeat { get; set; } = 0.5;
        public string Symbol { get; set; } = "BTCUSDT";
        public double PercentChange { get; set; } = 1;
    }

    public partial class MainWindow : Window
    {

        private DispatcherTimer dispatchTimer = new DispatcherTimer();

        TextBoxOutputter textOutputter;

        public MainWindow()
        {

            InitializeComponent();
            Details details = new Details();
            this.DataContext = details;
            
            dispatchTimer.Tick += APICall;
            dispatchTimer.Interval = TimeSpan.FromSeconds(new Details().Repeat);
            dispatchTimer.Start();

        }


        public async void APICall(object sender, EventArgs e)
        {
            
            //You must populate the the apiKey with your own from Binance.com, as well as the secret key.
            string api_Key = "API key here";
            string secretKey = "Secret key here";


            var client = new BinanceClient(new ClientConfiguration()
            {
                ApiKey = api_Key,
                SecretKey = secretKey,
                
            });
            
            var demo = new PriceAlert();
            await demo.PriceDifferenceAlert(new Details().Symbol, client, new Details().PercentChange);
            textOutputter = new TextBoxOutputter(PriceBox);
            Console.SetOut(textOutputter);

            //Delay the price wipe from the TextBox.
            Thread.Sleep(5000);
            PriceBox.Text = string.Empty;

        }
                
    }
}

