using MailInterface;
using StockRequestInterface;

namespace StockMonitorInterface
{
    internal class StockMonitor
    {
        private readonly double priceToSell, priceToBuy;
        private readonly string stockName;
        private readonly StockRequest stockRequest;
        private readonly Mail mail;
        private double currentPrice;

        enum QuoteStatus
        {
            Stay,
            Buy,
            Sell
        };


        private QuoteStatus quoteStatus = QuoteStatus.Stay;
        public StockMonitor(string stockName, double priceToSell, double priceToBuy)
        {
            Console.WriteLine("\nStock name: " + stockName +
                                "\nQuote to \n\tsell: " + priceToSell +
                                "\n\tbuy: " + priceToBuy + "\n");

            this.priceToSell = priceToSell;
            this.priceToBuy = priceToBuy;
            this.stockName = stockName;
            this.stockRequest = new StockRequest();
            this.mail = new Mail();
        }

        public void Monitor()
        {
            while (true)
            {
                this.currentPrice = this.stockRequest.GetPrice(this.stockName);
                Console.Write($@" * Current price {this.currentPrice}: ");

                // TODO: Send email in async mode

                if (this.currentPrice >= this.priceToSell)
                {
                    if (quoteStatus != QuoteStatus.Sell)
                    {
                        quoteStatus = QuoteStatus.Sell;

                        string subject = "It's time to sell " + this.stockName;
                        string msg = "Hello,\n\n" +
                            "It's time to sell " + this.stockName + ".\n" +
                            "Stock quote: R$ " + this.currentPrice + ".\n\n" +
                            "Att,\nStock Quote Alert.";

                        Console.WriteLine(subject);
                        this.mail.Send(subject, msg);
                    }
                }
                else if (this.currentPrice <= this.priceToBuy)
                {
                    if (quoteStatus != QuoteStatus.Buy)
                    {
                        quoteStatus = QuoteStatus.Buy;

                        string subject = "It's time to buy " + this.stockName;
                        string msg = "Hello,\n\n" +
                            "It's time to buy " + this.stockName + ".\n" +
                            "Stock quote: R$ " + this.currentPrice + ".\n\n" +
                            "Att,\nStock Quote Alert.";

                        Console.WriteLine(subject);
                        this.mail.Send(subject, msg);
                    }
                }
                else
                {
                    quoteStatus = QuoteStatus.Stay;
                }

                Thread.Sleep(15000);
            }
        }

    }
}
