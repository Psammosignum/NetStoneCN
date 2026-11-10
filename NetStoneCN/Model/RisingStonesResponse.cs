using Newtonsoft.Json;

namespace NetStoneCN.Model;

internal sealed class RisingStonesResponse<T>
{
    [JsonProperty("code")] public int Code { get; set; }
    [JsonProperty("msg")] public string? Message { get; set; }
    [JsonProperty("data")] public T? Data { get; set; }
}
