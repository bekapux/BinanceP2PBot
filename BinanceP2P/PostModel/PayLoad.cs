public class PayLoad
{
  public string Asset { get; set; } = "USDT";
  public string Fiat { get; set; } = "USD";
  public int Page { get; set; } = 1;
  public List<string> PayTypes { get; set; } = new List<string> { "TBCbank" };
  public string PublisherType { get; set; } = "merchant";
  public int Rows { get; set; } = 10;
  public string TradeType { get; set; } = "SELL";
}
