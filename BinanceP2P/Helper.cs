using Newtonsoft.Json;
using RestSharp;

namespace BinanceP2P
{
  public static class Helper
  {
    public static void PrintResults(IEnumerable<Datum>? Results)
    {
      Console.Clear();
      if (Results?.Any() == true)
      {
        Console.WriteLine("Price      | Avaliable    |             Nickname|");
        Console.WriteLine("-----------|--------------|---------------------|");
        foreach (var item in Results)
        {
          Console.WriteLine(
            String.Format("{0,-10} | {1,-12} | {2, 20}|",
           "$ " + item.adv.price,
           "$ " + item.adv.dynamicMaxSingleTransAmount,
            item.advertiser.nickName
          ));
        }
      }
      else
      {
        Console.WriteLine("Not Found");
      }
    }

    public static async Task<IEnumerable<Datum>> GetDataFromAPI(string BaseUrl, string QueryString)
    {
      try
      {
        var Client = new RestClient(BaseUrl);
        var Request = new RestRequest(QueryString);
        Request.RequestFormat = DataFormat.Json;
        Request.AddJsonBody(new PayLoad());
        var Response = await Client.ExecutePostAsync(Request);

        var ConvertedResults = JsonConvert.DeserializeObject<Response>(Response?.Content!);

        var Result = ConvertedResults?.data
            .Where(x =>
              x.advertiser.monthOrderCount > Requirements.MinTransactionCount &&
              Convert.ToDecimal(x.adv.price) > Requirements.MinAcceptedPrice &&
              x.advertiser.monthFinishRate > Requirements.TransactionFinishRate
        );

        return Result;
      }
      catch (Exception ex)
      {
        var ragac = ex.GetType().Name;
        if(ex is ArgumentNullException)
        {
          Console.WriteLine("Something went wrong");
        }
        return new List<Datum>();
      }
    }

    public static async Task DelaySeconds(int seconds)
    {
      await Task.Delay(seconds*1000);
    }
  }
}
