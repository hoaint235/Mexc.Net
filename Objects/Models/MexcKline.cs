﻿namespace Mexc.NET.Objects.Models;

/// <summary>
/// Kline info
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class MexcKline
{
    /// <summary>
    /// The start time of the kline
    /// </summary>
    [ArrayProperty(0), JsonConverter(typeof(DateTimeConverter))]
    public DateTime OpenTime { get; set; }
    /// <summary>
    /// The open price
    /// </summary>
    [ArrayProperty(1)]
    public decimal OpenPrice { get; set; }
    /// <summary>
    /// The highest price during this kline
    /// </summary>
    [ArrayProperty(2)]
    public decimal HighPrice { get; set; }
    /// <summary>
    /// The lowest price during this kline
    /// </summary>
    [ArrayProperty(3)]
    public decimal LowPrice { get; set; }
    /// <summary>
    /// The close price
    /// </summary>
    [ArrayProperty(4)]
    public decimal ClosePrice { get; set; }
    /// <summary>
    /// The volume of the kline
    /// </summary>
    [ArrayProperty(5)]
    public decimal Volume { get; set; }
    /// <summary>
    /// The start time of the kline
    /// </summary>
    [ArrayProperty(6), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CloseTime { get; set; }
    /// <summary>
    /// The volume of the kline
    /// </summary>
    [ArrayProperty(7)]
    public decimal QuoteVolume { get; set; }
}
