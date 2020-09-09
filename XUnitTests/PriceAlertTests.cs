using BinanceExchange.API.Client;
using BinancialServices;
using Microsoft.VisualBasic.FileIO;
using NuGet.Frameworks;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Xunit;

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
