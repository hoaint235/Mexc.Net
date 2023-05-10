namespace Mexc.NET.Objects.Models.Spot.Socket;

/// <summary>
/// Candlestick information for symbol
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class MexcSpotKline : MexcKlineBase
{ }