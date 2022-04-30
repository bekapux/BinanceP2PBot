using Newtonsoft.Json;
using RestSharp;

namespace BinanceP2P
{
  public static class Helper
  {
    public static void PrintResults(IEnumerable<Datum>? Results)
    {
      if (Results?.Any() == true)
      {
        Console.WriteLine("Price      | Avaliable  |             Nickname|");
        Console.WriteLine("-----------|------------|---------------------|");
        foreach (var item in Results)
        {
          Console.WriteLine(
            String.Format("{0,-10} | {1,-10} | {2, 20}|",
            item.adv.price,
            item.adv.dynamicMaxSingleTransAmount,
            item.advertiser.nickName
          ));
        }
      }
    }

    public static async Task<IEnumerable<Datum>> GetData(string BaseUrl, string QueryString)
    {
      var Client = new RestClient(BaseUrl);
      var Request = new RestRequest(QueryString);
      Request.RequestFormat = DataFormat.Json;
      Request.AddJsonBody(new PayLoad());
      var Response = await Client.ExecutePostAsync(Request);

      var Results = JsonConvert.DeserializeObject<Response>(Response?.Content!)?.data
          .Where(x =>
            x.advertiser.monthOrderCount > Requirements.MinTransactionCount &&
            Convert.ToDecimal(x.adv.price) > Requirements.MinAcceptedPrice &&
            x.advertiser.monthFinishRate > Requirements.TransactionFinishRate
      );

      return Results!;
    }
  }
}
