# cryptocurrency-ticker

# Introduction

This is a WPF desktop application that you can run in the background, which will pull price data from 
Binance.com exchange API (within defined regular intervals) and presents them to the user. You can also elect to only show prices
if it is above a certain percentage change, as well as add an audio notification (which could be useful if you're a trader). 

# How to use
Using the application is very straigth forward, select your currency pair, percentage change and how often you want to pull 
the data by modifying the property values in the Details class. More details can be found on the [summary] in the 
repository.

# Sample code

```c# 
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
        public string Symbol { get; set; } = "LTCUSDT";
        public double PercentChange { get; set; } = 0;
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
            string api_Key = "Api key here";
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
```

<image src="https://github.com/ilhan-uk/cryptocurrency-ticker/blob/master/Capture1.jpg" width=400 height=500>

# Unit tests
```c#
namespace XUnitTests
{
    public class PriceAlertTests
    {
       
        [Fact]
        public async Task PriceAlertFalseParameters()
        {
            //Arrange
            var pa = new PriceAlert();
            var democlient = new BinanceClient(new ClientConfiguration()
            {
                ApiKey = "84xsJ1uVfJqlOnLl0tfLgAU2lU0JlBbSGlpctDg7etViig4XH8OGITP0hZfCO8LQ",
                SecretKey = "f3ht3nBh8x9k2FWgZSXa9GWV1dHVmv4GE3x3dFIP95ILMkujNmurKg9Eb4npuGUk"
            });

            //Act
            var exceptionRecorded = await Record.ExceptionAsync(async () => await pa.PriceDifferenceAlert("BTC/USDT", democlient, 1.0));
            string expString = exceptionRecorded.ToString();
            
            //Assert
            Assert.Contains("Malformed requests are sent to the server. Please review the request object/string", expString);
            
        }

        [Fact]
        public async Task PriceAlertCorrect()
        {
            //Arrange
            var pa = new PriceAlert();
            var democlient = new BinanceClient(new ClientConfiguration()
            {
                ApiKey = "api key here",
                SecretKey = "secret key here"
            });

            //Act
            var exceptionRecorded = await Record.ExceptionAsync(async () => await pa.PriceDifferenceAlert("BTCUSDT", democlient, 1.0));
            
            //Assert
            Assert.Null(exceptionRecorded);

        }

    }
}
```
# Prerequisite 
This was made on C# 7, Visual Studio 2019 version 16.6.4. Very little knowledge of C# is required to install and run. 
You will need a Binance exchange account to operate the applciation as it requires private keys.

# Acknowledgement
This application makes use of BinanceDotNet API implementation by glitch100.
