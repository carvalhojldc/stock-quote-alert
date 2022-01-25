using Newtonsoft.Json;

namespace StockRequestInterface
{
    internal class StockRequest
    {
        private string apiKey;
        private readonly HttpClient httpClient = new HttpClient();

        public StockRequest()
        {
            try
            {
                string text = File.ReadAllText(@"api.config");
                var data = JsonConvert.DeserializeObject<dynamic>(text);
                if (data != null)
                {
                    this.apiKey = data.SelectToken("key");
                }

                if (this.apiKey == null)
                {
                    Console.WriteLine("\tERROR API config not set");
                    Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\tERROR " + ex.Message);
                Environment.Exit(1);
            }
        }

        private async Task<string> GetStockInfo(string stockName)
        {
            var stringTask =
                httpClient.GetStringAsync("https://" +
                @$"fcsapi.com/api-v3/stock/latest?country=Brazil&symbol={stockName}&access_key={this.apiKey}");
            return await stringTask;
        }

        public double GetPrice(string stockName)
        {
            Task<string> result = GetStockInfo(stockName);
            string stockInfo = result.Result;

            try
            {
                var data = JsonConvert.DeserializeObject<dynamic>(stockInfo);
                if (data != null)
                {
                    var bovespaInfo = data["response"][0];
                    if (bovespaInfo != null)
                    {
                        return Convert.ToDouble(bovespaInfo["c"]);
                    }
                    else
                    {
                        Console.WriteLine("\tERROR in server response");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\tERROR " + ex.Message);
            }

            return 0;
        }
    }
}
