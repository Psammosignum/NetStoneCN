using Newtonsoft.Json;

namespace NetStoneCN.Model;

/// <summary>石之家角色搜索结果中的一条角色记录</summary>
public sealed class RisingStoneCharacterSearch
{
    /// <summary>角色 ID</summary>
    [JsonProperty("uuid")] public string? Uuid { get; set; }
    /// <summary>头像链接地址，无头像时为 null，由调用方显示默认图片</summary>
    [JsonProperty("avatar")] public Uri? Avatar { get; set; }
    /// <summary>角色名称</summary>
    [JsonProperty("character_name")] public string? CharacterName { get; set; }
    /// <summary>大区名称</summary>
    [JsonProperty("area_name")] public string? DataCenterName { get; set; }
    /// <summary>服务器名称</summary>
    [JsonProperty("group_name")] public string? ServerName { get; set; }
    /// <summary>简介</summary>
    [JsonProperty("profile")] public string? Profile { get; set; }
    /// <summary>封测徽章类型</summary>
    [JsonProperty("test_limited_badge")] public int? TestLimitedBadge { get; set; }
    /// <summary>创作者徽章类型</summary>
    [JsonProperty("posts2_creator_badge")] public int? Posts2CreatorBadge { get; set; }
    /// <summary>版主或管理员标记</summary>
    [JsonProperty("admin_tag")] public int? AdminTag { get; set; }
    /// <summary>粉丝数</summary>
    [JsonProperty("fansNum")] public int? FansNum { get; set; }
    /// <summary>当前请求用户与该角色的关系代码</summary>
    [JsonProperty("relation")] public int? Relation { get; set; }
}
