using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BinanceExchange.API;
using BinanceExchange.API.Client;
using BinanceExchange.API.Client.Interfaces;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Market;
using BinanceExchange.API.Models.Request;
using BinanceExchange.API.Models.Response;
using BinanceExchange.API.Models.Response.Error;
using BinanceExchange.API.Models.WebSocket;
using BinanceExchange.API.Utility;
using BinanceExchange.API.Websockets;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSocketSharp;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using System.Media;

namespace BinancialServices
{

    public class PriceAlert
    {
                

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_symbol"> This is the exchange pair you wish to use, make sure it's in this "BTCETH" format and 
        ///                         not BTC/ETH. Check Binance's website if errors persist </param>
        /// <param name="client">You must instanciate an instance of the BinanceClient, one is already available on line
        ///                         71 in MainWindow.xaml.cs </param>
        /// <param name="targetPercent">This is the percentage change you wish to set. If the percentage change in equal
        ///                             or greater than this number, only then will the price and percent change be displayed
        ///                             you can also choose whether to set an audio alert. Bare in mind that audio alert will continue until to shut 
        ///                             the program or the percent change follows below your target rate.</param>
        /// <returns></returns>
        
        public async Task<object> PriceDifferenceAlert(string _symbol, BinanceClient client, double targetPercent = 0)
        {

            var dailyTicker = await client.GetDailyTicker(_symbol);
            while (client != null)
            {

                var jsonObj = JsonConvert.SerializeObject(dailyTicker.PriceChangePercent, Formatting.Indented);
                var changePercent = Convert.ToDouble(jsonObj);
                if (changePercent >= targetPercent)
                {
                    Console.WriteLine($"Percentage change is: {changePercent} for {_symbol}                            ");

                    // Delete this if statement completely if you DO NOT wish to set off a continuous
                    // audio if your target percent change is reached. It is "comment out" by default.
                    //SystemSounds.Hand.Play();
                }

                break;

            }
            return Task.CompletedTask;
        }

    }

}
