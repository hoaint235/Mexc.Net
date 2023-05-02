namespace Mexc.NET.Objects.Models;

public class MexcCheckTime
{
    [JsonProperty("serverTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime ServerTime { get; set; }
}
