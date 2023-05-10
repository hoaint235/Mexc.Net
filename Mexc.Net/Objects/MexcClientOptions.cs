namespace Mexc.NET.Objects;

/// <summary>
/// Options for the MexcClient
/// </summary>
public class MexcClientOptions : ClientOptions
{
    /// <summary>
    /// Default options for the spot client
    /// </summary>
    public static MexcClientOptions Default { get; set; } = new MexcClientOptions();

    /// <summary>
    /// The default receive window for requests
    /// </summary>
    public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

    private MexcRestApiClientOptions _spotApiOptions = new(MexcApiAddresses.Default.SpotAddress)
    {
        RateLimiters = new List<IRateLimiter>
        {
            new RateLimiter()
                .AddPartialEndpointLimit("/api/v3/orders", 180, TimeSpan.FromSeconds(3), null, true, true)
                .AddApiKeyLimit(200, TimeSpan.FromSeconds(10), true, true)
                .AddTotalRateLimit(100, TimeSpan.FromSeconds(10))
        }
    };

    /// <inheritdoc />
    public new MexcApiCredentials? ApiCredentials
    {
        get => (MexcApiCredentials?)base.ApiCredentials;
        set => base.ApiCredentials = value;
    }

    /// <summary>
    /// Spot API options
    /// </summary>
    public MexcRestApiClientOptions SpotApiOptions
    {
        get => _spotApiOptions;
        set => _spotApiOptions = new MexcRestApiClientOptions(_spotApiOptions, value);
    }

    /// <summary>
    /// ctor
    /// </summary>
    public MexcClientOptions() : this(Default)
    {
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="baseOn">Base the new options on other options</param>
    internal MexcClientOptions(MexcClientOptions baseOn) : base(baseOn)
    {
        if (baseOn == null)
            return;
        ApiCredentials = (MexcApiCredentials?)baseOn.ApiCredentials?.Copy();

        _spotApiOptions = new MexcRestApiClientOptions(baseOn.SpotApiOptions, null);
    }
}

/// <summary>
/// Options for the MexcSocketClient
/// </summary>
public class MexcSocketClientOptions : ClientOptions
{
    /// <summary>
    /// Default options for the spot client
    /// </summary>
    public static MexcSocketClientOptions Default { get; set; } = new MexcSocketClientOptions();

    /// <inheritdoc />
    public new MexcApiCredentials? ApiCredentials
    {
        get => (MexcApiCredentials?)base.ApiCredentials;
        set => base.ApiCredentials = value;
    }

    private MexcSocketApiClientOptions _spotStreamsOptions = new(MexcApiAddresses.Default.SocketClientAddress)
    {
        SocketSubscriptionsCombineTarget = 10,
        MaxSocketConnections = 30
    };

    /// <summary>
    /// Spot stream options
    /// </summary>
    public MexcSocketApiClientOptions SpotStreamsOptions
    {
        get => _spotStreamsOptions;
        set => _spotStreamsOptions = new MexcSocketApiClientOptions(_spotStreamsOptions, value);
    }

    private MexcSocketApiClientOptions _futuresStreamsOptions = new MexcSocketApiClientOptions()
    {
        SocketSubscriptionsCombineTarget = 10,
        MaxSocketConnections = 30
    };

    /// <summary>
    /// Futures stream options
    /// </summary>
    public MexcSocketApiClientOptions FuturesStreamsOptions
    {
        get => _futuresStreamsOptions;
        set => _futuresStreamsOptions = new MexcSocketApiClientOptions(_futuresStreamsOptions, value);
    }

    /// <summary>
    /// ctor
    /// </summary>
    public MexcSocketClientOptions() : this(Default)
    {
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="baseOn">Base the new options on other options</param>
    internal MexcSocketClientOptions(MexcSocketClientOptions baseOn) : base(baseOn)
    {
        if (baseOn == null)
            return;
        
        ApiCredentials = (MexcApiCredentials?)baseOn.ApiCredentials?.Copy();
        _spotStreamsOptions = new MexcSocketApiClientOptions(baseOn.SpotStreamsOptions, null);
        _futuresStreamsOptions = new MexcSocketApiClientOptions(baseOn.FuturesStreamsOptions, null);
    }
}

/// <summary>
/// Mexc rest client options
/// </summary>
public class MexcRestApiClientOptions : RestApiClientOptions
{
    /// <inheritdoc />
    public new MexcApiCredentials? ApiCredentials
    {
        get => (MexcApiCredentials?)base.ApiCredentials;
        set => base.ApiCredentials = value;
    }

    /// <summary>
    /// Whether to check the trade rules when placing new orders and what to do if the trade isn't valid
    /// </summary>
    public TradeRulesBehaviour TradeRulesBehaviour { get; set; } = TradeRulesBehaviour.None;

    /// <summary>
    /// How often the trade rules should be updated. Only used when TradeRulesBehaviour is not None
    /// </summary>
    public TimeSpan TradeRulesUpdateInterval { get; set; } = TimeSpan.FromMinutes(60);

    /// <summary>
    /// ctor
    /// </summary>
    public MexcRestApiClientOptions()
    {
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="baseAddress"></param>
    internal MexcRestApiClientOptions(string baseAddress) : base(baseAddress)
    {
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="baseOn"></param>
    /// <param name="newValues"></param>
    internal MexcRestApiClientOptions(MexcRestApiClientOptions baseOn, MexcRestApiClientOptions? newValues) : base(baseOn, newValues)
    {
        ApiCredentials = (MexcApiCredentials?)newValues?.ApiCredentials?.Copy() ?? (MexcApiCredentials?)baseOn.ApiCredentials?.Copy();
    }
}

/// <summary>
/// Socket client options
/// </summary>
public class MexcSocketApiClientOptions : SocketApiClientOptions
{
    /// <inheritdoc />
    public new MexcApiCredentials? ApiCredentials
    {
        get => (MexcApiCredentials?)base.ApiCredentials;
        set => base.ApiCredentials = value;
    }

    /// <summary>
    /// ctor
    /// </summary>
    public MexcSocketApiClientOptions()
    {
    }

    /// <summary>
    /// ctor
    /// </summary>
    public MexcSocketApiClientOptions(string baseAddress) : base(baseAddress)
    {
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="baseOn"></param>
    /// <param name="newValues"></param>
    internal MexcSocketApiClientOptions(MexcSocketApiClientOptions baseOn, MexcSocketApiClientOptions? newValues) : base(baseOn, newValues)
    {
        ApiCredentials = (MexcApiCredentials?)newValues?.ApiCredentials?.Copy() ?? (MexcApiCredentials?)baseOn.ApiCredentials?.Copy();
    }
}

/// <summary>
/// Options for the MexcSymbolOrderBook
/// </summary>
public class MexcOrderBookOptions : OrderBookOptions
{
    /// <summary>
    /// The top amount of results to keep in sync. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
    /// </summary>
    public TimeSpan? InitialDataTimeout { get; set; }

    /// <summary>
    /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
    /// </summary>
    public IMexcSocketClient? SocketClient { get; set; }

    /// <summary>
    /// The client to use for the initial order book request
    /// </summary>
    public IMexcClient? RestClient { get; set; }
}
