
using BinanceP2P;

public class Response
{
  public string code { get; set; }
  public object message { get; set; }
  public object messageDetail { get; set; }
  public Datum[] data { get; set; }
  public int total { get; set; }
  public bool success { get; set; }
}