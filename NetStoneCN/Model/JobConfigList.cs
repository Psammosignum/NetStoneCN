using Newtonsoft.Json;

namespace NetStoneCN.Model;

/// <summary>石之家招募接口返回的职业配置集合，键为分类名称</summary>
public sealed class JobConfigList : Dictionary<string, List<JobConfigItem>>
{
}

/// <summary>职业配置条目</summary>
public sealed class JobConfigItem
{
    /// <summary>职业或职能编号</summary>
    [JsonProperty("id")] public string? Id { get; set; }
    /// <summary>职业或职能名称</summary>
    [JsonProperty("value")] public string? Value { get; set; }
    /// <summary>职业图标地址</summary>
    [JsonProperty("job_pic_url")] public Uri? JobPicUrl { get; set; }
    /// <summary>该条目的职能分类名称</summary>
    [JsonProperty("job_type")] public string? JobType { get; set; }
}
