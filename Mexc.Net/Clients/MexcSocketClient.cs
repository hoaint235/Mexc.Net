namespace Mexc.NET.Clients;

public class MexcSocketClient : BaseSocketClient, IMexcSocketClient
{
    public IMexcSocketClientSpotStreams SpotStreams { get; set; }

    /// <summary>
    /// Create a new instance of MexcSocketClientSpot with default options
    /// </summary>
    public MexcSocketClient() : this(MexcSocketClientOptions.Default)
    {
    }

    /// <summary>
    /// Create a new instance of MexcSocketClientSpot using provided options
    /// </summary>
    /// <param name="options">The options to use for this client</param>
    public MexcSocketClient(MexcSocketClientOptions options) : base("Mexc", options)
    {
        SpotStreams = AddApiClient(new MexcSocketClientSpotStreams(log, options));
    }

    /// <summary>
    /// Set the default options to be used when creating new clients
    /// </summary>
    /// <param name="options">Options to use as default</param>
    public static void SetDefaultOptions(MexcSocketClientOptions options)
    {
        MexcSocketClientOptions.Default = options;
    }

    /// <inheritdoc />
    public void SetApiCredentials(MexcApiCredentials credentials)
    {
        SpotStreams.SetApiCredentials(credentials);
    }
}
