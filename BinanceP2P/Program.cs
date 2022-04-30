using BinanceP2P;

var ReceivedData = await Helper.GetData(
  BaseUrl: "https://c2c.binance.com/", 
  QueryString: "bapi/c2c/v2/friendly/c2c/adv/search"
);

while (true)
{
  Helper.PrintResults(ReceivedData);
  await Task.Delay(60000);
}
