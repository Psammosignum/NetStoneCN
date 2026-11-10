using Newtonsoft.Json;

namespace NetStoneCN.Model;

/// <summary>当前登录账号绑定的角色及账号资料</summary>
public sealed class CurrentCharacter
{
    /// <summary>石之家角色 UUID</summary>
    [JsonProperty("uuid")] public string? Uuid { get; set; }
    /// <summary>游戏角色 ID</summary>
    [JsonProperty("character_id")] public string? CharacterId { get; set; }
    /// <summary>角色名称</summary>
    [JsonProperty("character_name")] public string? Name { get; set; }
    /// <summary>大区 ID</summary>
    [JsonProperty("area_id")] public int? DataCenterId { get; set; }
    /// <summary>大区名称</summary>
    [JsonProperty("area_name")] public string? DataCenterName { get; set; }
    /// <summary>服务器 ID</summary>
    [JsonProperty("group_id")] public int? ServerId { get; set; }
    /// <summary>服务器名称</summary>
    [JsonProperty("group_name")] public string? ServerName { get; set; }
    /// <summary>角色头像地址</summary>
    [JsonProperty("avatar")] public Uri? Avatar { get; set; }
    /// <summary>角色简介</summary>
    [JsonProperty("profile")] public string? Profile { get; set; }
    /// <summary>工作日在线时间偏好</summary>
    [JsonProperty("weekday_time")] public string? WeekdayTime { get; set; }
    /// <summary>周末在线时间偏好</summary>
    [JsonProperty("weekend_time")] public string? WeekendTime { get; set; }
    /// <summary>QQ 联系方式</summary>
    [JsonProperty("qq")] public string? Qq { get; set; }

    /// <summary>职业信息公开设置</summary>
    [JsonProperty("career_publish")] public int? CareerPublish { get; set; }
    /// <summary>部队信息公开设置</summary>
    [JsonProperty("guild_publish")] public int? GuildPublish { get; set; }
    /// <summary>创建时间公开设置</summary>
    [JsonProperty("create_time_publish")] public int? CreateTimePublish { get; set; }
    /// <summary>最后登录时间公开设置</summary>
    [JsonProperty("last_login_time_publish")] public int? LastLoginTimePublish { get; set; }
    /// <summary>游戏时长公开设置</summary>
    [JsonProperty("play_time_publish")] public int? PlayTimePublish { get; set; }
    /// <summary>房屋信息公开设置</summary>
    [JsonProperty("house_info_publish")] public int? HouseInfoPublish { get; set; }
    /// <summary>洗澡次数公开设置</summary>
    [JsonProperty("washing_num_publish")] public int? WashingNumPublish { get; set; }
    /// <summary>成就信息公开设置</summary>
    [JsonProperty("achieve_publish")] public int? AchievePublish { get; set; }
    /// <summary>近期信息公开设置</summary>
    [JsonProperty("resently_publish")] public int? RecentlyPublish { get; set; }
    /// <summary>账号经验值</summary>
    [JsonProperty("experience")] public string? Experience { get; set; }
    /// <summary>资料主题 ID</summary>
    [JsonProperty("theme_id")] public int? ThemeId { get; set; }
    /// <summary>账号状态值</summary>
    [JsonProperty("status")] public int? Status { get; set; }
    /// <summary>账号创建时间</summary>
    [JsonProperty("created_at")] public string? CreatedAt { get; set; }
    /// <summary>账号资料更新时间</summary>
    [JsonProperty("updated_at")] public string? UpdatedAt { get; set; }
    /// <summary>最后更新资料的角色名称</summary>
    [JsonProperty("updated_by")] public string? UpdatedBy { get; set; }
    /// <summary>最近登录时间</summary>
    [JsonProperty("last_login_time")] public string? LastLoginTime { get; set; }
    /// <summary>封测徽章值</summary>
    [JsonProperty("test_limited_badge")] public int? TestLimitedBadge { get; set; }
    /// <summary>创作者徽章值</summary>
    [JsonProperty("posts2_creator_badge")] public int? Posts2CreatorBadge { get; set; }
    /// <summary>管理员标记</summary>
    [JsonProperty("admin_tag")] public int? AdminTag { get; set; }
    /// <summary>已公开的资料页签</summary>
    [JsonProperty("publish_tab")] public string? PublishTab { get; set; }
    /// <summary>已公开的成就页签</summary>
    [JsonProperty("achieve_tab")] public string? AchieveTab { get; set; }
    /// <summary>藏宝图次数公开设置</summary>
    [JsonProperty("treasure_times_publish")] public int? TreasureTimesPublish { get; set; }
    /// <summary>击杀次数公开设置</summary>
    [JsonProperty("kill_times_publish")] public int? KillTimesPublish { get; set; }
    /// <summary>新排行公开设置</summary>
    [JsonProperty("newrank_publish")] public int? NewRankPublish { get; set; }
    /// <summary>水晶冲突段位公开设置</summary>
    [JsonProperty("crystal_rank_publish")] public int? CrystalRankPublish { get; set; }
    /// <summary>捕鱼次数公开设置</summary>
    [JsonProperty("fish_times_publish")] public int? FishTimesPublish { get; set; }
    /// <summary>崩之家徽章值</summary>
    [JsonProperty("collapse_badge")] public int? CollapseBadge { get; set; }
    /// <summary>狒狒徽章值</summary>
    [JsonProperty("feifei_badge")] public int? FeifeiBadge { get; set; }
    /// <summary>徽章 JSON 字符串</summary>
    [JsonProperty("badge")] public string? BadgeJson { get; set; }
    /// <summary>当前角色详细资料</summary>
    [JsonProperty("characterDetail")] public RisingStonesCharacterDetail? CharacterDetail { get; set; }
    /// <summary>角色是否已签到</summary>
    [JsonProperty("isSign")] public int? IsSigned { get; set; }
    /// <summary>系统消息数量</summary>
    [JsonProperty("sysNum")] public int? SystemMessageCount { get; set; }
    /// <summary>被提及次数</summary>
    [JsonProperty("atNum")] public int? MentionCount { get; set; }
    /// <summary>评论数量</summary>
    [JsonProperty("commentNum")] public int? CommentCount { get; set; }
    /// <summary>收到的点赞数</summary>
    [JsonProperty("beLikedNum")] public int? LikeCount { get; set; }
    /// <summary>未读提及消息数</summary>
    [JsonProperty("atMsgNum")] public int? UnreadMentionCount { get; set; }
    /// <summary>未读评论消息数</summary>
    [JsonProperty("commentMsgNum")] public int? UnreadCommentCount { get; set; }
    /// <summary>未读点赞消息数</summary>
    [JsonProperty("beLikedMsgNum")] public int? UnreadLikeCount { get; set; }
    /// <summary>石之家平台编号</summary>
    [JsonProperty("platform")] public int? Platform { get; set; }
}
