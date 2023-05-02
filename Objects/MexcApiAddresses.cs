namespace Mexc.NET.Objects;

/// <summary>
/// Mexc API addresses
/// </summary>
public class MexcApiAddresses
{
    /// <summary>
    /// The address used by the MexcClient for the SPOT API
    /// </summary>
    public string SpotAddress { get; set; } = string.Empty;

    /// <summary>
    /// The address used by the MexcClient for the futures API
    /// </summary>
    public string FuturesAddress { get; set; } = string.Empty;

    /// <summary>
    /// The default addresses to connect to the Mexc.com API
    /// </summary>
    public static MexcApiAddresses Default = new()
    {
        SpotAddress = "https://api.mexc.com/api/",
    };
}
