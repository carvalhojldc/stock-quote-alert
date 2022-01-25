using Newtonsoft.Json;

namespace StockRequestInterface
{
    internal class StockRequest
    {
        private readonly string apiKey = "";
        private readonly HttpClient httpClient = new HttpClient();

        public StockRequest()
        {
            // TODO: Read API key from configs
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
                var bovespaInfo = data["response"][0];
                if (bovespaInfo != null)
                {
                    return Convert.ToDouble(bovespaInfo["c"]);
                }
                else
                {
                    Console.WriteLine("\tERROR in server response");
                    return 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("\tERROR " + ex.Message);
                return 0;
            }
        }
    }
}
