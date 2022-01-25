# Stock Quote Alert

This project checks the stock price (from [Forex API](https://fcsapi.com/))  and sends a notification email if it is at the buy or sell quote. 

**Limitations:** By using a free Forex account, by default the requests are not in real time.

## How Run?

Configure the files below in the same directory as the executable:

* **api.config**: Access key of [Forex API](https://fcsapi.com/) 
* **smtp.config**: SNMT server configuration to send notifications

Run the binary:

```
> stock-quote-alert.exe Arg1 Arg2 Arg3

When:
    Arg1: Stock name
    Arg2: Price to sell
    Arg3: Price to buy

Example:

> stock-quote-alert.exe PETR4 22.67 22.59
```