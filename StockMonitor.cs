using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockRequestInterface;

namespace StockMonitorInterface
{
    internal class StockMonitor
    {
        private readonly double priceToSell, priceToBuy;
        private readonly string stockName;
        private readonly StockRequest stockRequest;
        private double currentPrice;

        public StockMonitor(string stockName, double priceToSell, double priceToBuy)
        {
            Console.WriteLine("\nStock name: " + stockName+ 
                                "\nQuote to \n\tsell: " + priceToSell + 
                                "\n\tbuy: " + priceToBuy + "\n");

            this.priceToSell = priceToSell;
            this.priceToBuy = priceToBuy;
            this.stockName = stockName;
            this.stockRequest = new StockRequest();
        }

        public void Monitor()
        {
            while (true)
            {
                this.currentPrice = this.stockRequest.GetPrice(this.stockName);
                Console.Write($@" * Current price {this.currentPrice}: ");

                if (this.currentPrice >= this.priceToSell)
                {
                    Console.WriteLine("Time to sell!");
                }
                else if (this.currentPrice <= this.priceToBuy)
                {
                    Console.WriteLine("Time to buy!");
                }

                Thread.Sleep(15000);
            }
        }

    }
}
