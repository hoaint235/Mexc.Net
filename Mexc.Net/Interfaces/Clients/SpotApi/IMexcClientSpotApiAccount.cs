namespace Mexc.NET.Interfaces.Clients.SpotApi;

public interface IMexcClientSpotApiAccount
{
    Task<WebCallResult<BinanceAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);
}
