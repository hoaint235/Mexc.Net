namespace Mexc.NET;

public static class MexcHelpers
{
    /// <summary>
    /// Add the IMexcClient and IMexcSocketClient to the service collection so they can be injected
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="defaultOptionsCallback">Set default options for the client</param>
    /// <param name="socketClientLifeTime">The lifetime of the IMexcSocketClient for the service collection. Defaults to Scoped.</param>
    /// <returns></returns>
    public static IServiceCollection AddMexc(
        this IServiceCollection services,
        Action<MexcClientOptions, MexcSocketClientOptions>? defaultOptionsCallback = null,
        ServiceLifetime? socketClientLifeTime = null)
    {
        if (defaultOptionsCallback != null)
        {
            var options = new MexcClientOptions();
            var socketOptions = new MexcSocketClientOptions();
            defaultOptionsCallback?.Invoke(options, socketOptions);

            MexcClient.SetDefaultOptions(options);
        }

        services.AddTransient<IMexcClient, MexcClient>();
        //if (socketClientLifeTime == null)
        //    services.AddScoped<IMexcSocketClient, MexcSocketClient>();
        //else
        //    services.Add(new ServiceDescriptor(typeof(IMexcSocketClient), typeof(MexcSocketClient), socketClientLifeTime.Value));
        return services;
    }

    /// <summary>
    /// Validate the string is a valid Mexc symbol.
    /// </summary>
    /// <param name="symbolString">string to validate</param>
    public static void ValidateMexcSymbol(this string symbolString)
    {
        if (string.IsNullOrEmpty(symbolString))
            throw new ArgumentException("Symbol is not provided");

        if (!Regex.IsMatch(symbolString, "^((([A-Z]|[0-9]){1,})(([A-Z]|[0-9]){1,}))$"))
            throw new ArgumentException($"{symbolString} is not a valid Mexc symbol. Should be [BaseAsset][QuoteAsset] e.g. ETHBTC");
    }

    /// <summary>
    /// Clamp a quantity between a min and max quantity and floor to the closest step
    /// </summary>
    /// <param name="minQuantity"></param>
    /// <param name="maxQuantity"></param>
    /// <param name="stepSize"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public static decimal ClampQuantity(decimal minQuantity, decimal maxQuantity, decimal stepSize, decimal quantity)
    {
        quantity = Math.Min(maxQuantity, quantity);
        quantity = Math.Max(minQuantity, quantity);
        if (stepSize == 0)
            return quantity;
        quantity -= quantity % stepSize;
        quantity = Floor(quantity);
        return quantity;
    }

    /// <summary>
    /// Clamp a price between a min and max price
    /// </summary>
    /// <param name="minPrice"></param>
    /// <param name="maxPrice"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal ClampPrice(decimal minPrice, decimal maxPrice, decimal price)
    {
        price = Math.Min(maxPrice, price);
        price = Math.Max(minPrice, price);
        return price;
    }

    /// <summary>
    /// Floor a price to the closest tick
    /// </summary>
    /// <param name="tickSize"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal FloorPrice(decimal tickSize, decimal price)
    {
        price -= price % tickSize;
        price = Floor(price);
        return price;
    }

    /// <summary>
    /// Floor
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static decimal Floor(decimal number)
    {
        return Math.Floor(number * 100000000) / 100000000;
    }
}
