using Newtonsoft.Json;

namespace NetStoneCN.Model;

/// <summary>获取登陆状态得到的简易信息</summary>
public sealed class LoginInformation
{
    /// <summary>玩家名</summary>
    [JsonProperty("character_name")] public string? Name { get; set; }
    /// <summary>玩家所在大区</summary>
    [JsonProperty("area_name")] public string? DataCenterName { get; set; }
    /// <summary>玩家所在服务器</summary>
    [JsonProperty("group_name")] public string? ServerName { get; set; }
}
