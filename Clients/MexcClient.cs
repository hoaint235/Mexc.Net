namespace Mexc.NET.Clients;

public class MexcClient : BaseRestClient, IMexcClient
{
    /// <inheritdoc />
    public IMexcClientSpotApi SpotApi { get; }

    public MexcClient() : this(MexcClientOptions.Default)
    { }

    public MexcClient(MexcClientOptions options) : base("Mexc", options)
    {
        SpotApi = AddApiClient(new MexcClientSpotApi(log, this, options));
    }

    /// <inheritdoc />
    public void SetApiCredentials(MexcApiCredentials credentials)
    {
        SpotApi.SetApiCredentials(credentials);
    }

    /// <summary>
    /// Set the default options to be used when creating new clients
    /// </summary>
    /// <param name="options">Options to use as default</param>
    public static void SetDefaultOptions(MexcClientOptions options)
    {
        MexcClientOptions.Default = options;
    }
}
