using BinanceP2P;

while (true)
{
  var ReceivedData = await Helper.GetDataFromAPI(
    BaseUrl: "https://c2c.binance.com/",
    QueryString: "bapi/c2c/v2/friendly/c2c/adv/search"
  );

  Helper.PrintResults(ReceivedData);

  await Helper.DelaySeconds(60);
}
