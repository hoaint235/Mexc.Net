namespace Mexc.NET.Clients.SpotApi;

public class MexcClientSpotApiAccount : IMexcClientSpotApiAccount
{
    private readonly string accountInfoEndpoint = "account";

    private readonly MexcClientSpotApi _baseClient;

    internal MexcClientSpotApiAccount(MexcClientSpotApi baseClient)
    {
        _baseClient = baseClient;
    }

    public async Task<WebCallResult<BinanceAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.Options.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

        return await _baseClient.SendRequestInternal<BinanceAccountInfo>(_baseClient.GetUri(accountInfoEndpoint), HttpMethod.Get, ct, parameters, true, weight: 10).ConfigureAwait(false);
    }
}