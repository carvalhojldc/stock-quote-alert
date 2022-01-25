namespace StockRequestInterface
{
    internal class StockRequest
    {
        /*
         * Using API from https://www.alphavantage.co/
         *
         * =========================================================================
         * Question from https://www.alphavantage.co/support/#api-key
         *
         * Are there usage/frequency limits for the API service?
         *
         * We are pleased to provide free stock API service for our global community \
         * of users for up to 5 API requests per minute and 500 requests per day. \
         * If you would like to target a larger API call volume, please \
         * visit premium membership.
         */

        public StockRequest()
        {

        }

        public float GetPrice(string stockName)
        {
            /*
             * Quote Endpoint Trending
             *
             * A lightweight alternative to the time series APIs, this service returns the
             * price and volume information for a token of your choice
             */
            return 12;
        }
    }
}
