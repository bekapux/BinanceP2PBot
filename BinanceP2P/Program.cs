using BinanceP2P;
using RestSharp;

var Client = new RestClient("https://c2c.binance.com/");
var Request = new RestRequest("bapi/c2c/v2/friendly/c2c/adv/search");
Request.RequestFormat = DataFormat.Json;
Request.AddJsonBody(new PayLoad());

var Results = await Helper.GetData(Client, Request);

while (true)
{
  Helper.PrintResults(Results);
  await Task.Delay(60000);
}

