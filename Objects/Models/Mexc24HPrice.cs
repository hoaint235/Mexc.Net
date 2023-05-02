﻿namespace Mexc.NET.Objects.Models;

/// <summary>
/// Price statistics of the last 24 hours
/// </summary>
public class Mexc24HPrice : Mexc24HPriceBase, IMexcTick
{
    /// <summary>
    /// The close price 24 hours ago
    /// </summary>
    [JsonProperty("prevClosePrice")]
    public decimal PrevDayClosePrice { get; set; }
    /// <summary>
    /// The best bid price in the order book
    /// </summary>
    [JsonProperty("bidPrice")]
    public decimal BestBidPrice { get; set; }
    /// <summary>
    /// The quantity of the best bid price in the order book
    /// </summary>
    [JsonProperty("bidQty")]
    public decimal BestBidQuantity { get; set; }
    /// <summary>
    /// The best ask price in the order book
    /// </summary>
    [JsonProperty("askPrice")]
    public decimal BestAskPrice { get; set; }
    /// <summary>
    /// The quantity of the best ask price in the order book
    /// </summary>
    [JsonProperty("AskQty")]
    public decimal BestAskQuantity { get; set; }

    /// <inheritdoc />
    public override decimal Volume { get; set; }
    /// <inheritdoc />
    [JsonProperty("quoteVolume")]
    public override decimal QuoteVolume { get; set; }
}