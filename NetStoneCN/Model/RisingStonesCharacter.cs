using Newtonsoft.Json;

namespace NetStoneCN.Model;

/// <summary>石之家用户资料接口返回的角色资料</summary>
public sealed class RisingStonesCharacter
{
    /// <summary>石之家数据库 ID</summary>
    [JsonProperty("id")] public long? Id { get; set; }
    /// <summary>角色 ID</summary>
    [JsonProperty("uuid")] public string? Uuid { get; set; }
    /// <summary>角色名称</summary>
    [JsonProperty("character_name")] public string? Name { get; set; }
    /// <summary>大区ID</summary>
    [JsonProperty("area_id")] public int? DataCenterId { get; set; }
    /// <summary>大区名称</summary>
    [JsonProperty("area_name")] public string? DataCenterName { get; set; }
    /// <summary>服务器ID</summary>
    [JsonProperty("group_id")] public int? ServerId { get; set; }
    /// <summary>服务器名称</summary>
    [JsonProperty("group_name")] public string? ServerName { get; set; }
    /// <summary>头像链接地址，无头像时为 null，由调用方显示默认图片</summary>
    [JsonProperty("avatar")] public Uri? Avatar { get; set; }
    /// <summary>简介</summary>
    [JsonProperty("profile")] public string? Profile { get; set; }
    /// <summary>用户设置的周中游玩时间</summary>
    [JsonProperty("weekday_time")] public string? WeekdayTime { get; set; }
    /// <summary>用户设置的周末游玩时间</summary>
    [JsonProperty("weekend_time")] public string? WeekendTime { get; set; }
    /// <summary>QQ 号</summary>
    [JsonProperty("qq")] public string? Qq { get; set; }

    /// <summary>是否展示职业信息(0或1，下同理)</summary>
    [JsonProperty("career_publish")] public int? CareerPublish { get; set; }
    /// <summary>是否展示部队信息</summary>
    [JsonProperty("guild_publish")] public int? GuildPublish { get; set; }
    /// <summary>是否展示创角时间</summary>
    [JsonProperty("create_time_publish")] public int? CreateTimePublish { get; set; }
    /// <summary>是否展示最后在线时间</summary>
    [JsonProperty("last_login_time_publish")] public int? LastLoginTimePublish { get; set; }
    /// <summary>是否展示游玩时长</summary>
    [JsonProperty("play_time_publish")] public int? PlayTimePublish { get; set; }
    /// <summary>是否展示房屋信息</summary>
    [JsonProperty("house_info_publish")] public int? HouseInfoPublish { get; set; }
    /// <summary>是否展示洗澡次数</summary>
    [JsonProperty("washing_num_publish")] public int? WashingNumPublish { get; set; }
    /// <summary>是否展示成就信息</summary>
    [JsonProperty("achieve_publish")] public int? AchievePublish { get; set; }
    /// <summary>是否展示近期信息</summary>
    [JsonProperty("resently_publish")] public int? RecentlyPublish { get; set; }
    /// <summary>账号经验</summary>
    [JsonProperty("experience")] public string? Experience { get; set; }
    /// <summary>使用的主题</summary>
    [JsonProperty("theme_id")] public int? ThemeId { get; set; }

    /// <summary>封测徽章类型</summary>
    [JsonProperty("test_limited_badge")] public int? TestLimitedBadge { get; set; }
    /// <summary>创作者徽章类型</summary>
    [JsonProperty("posts2_creator_badge")] public int? Posts2CreatorBadge { get; set; }
    /// <summary>版主/管理员显示</summary>
    [JsonProperty("admin_tag")] public int? AdminTag { get; set; }
    /// <summary>已公开的资料页签，以逗号分隔</summary>
    [JsonProperty("publish_tab")] public string? PublishTab { get; set; }
    /// <summary>已公开的成就页签，以逗号分隔</summary>
    [JsonProperty("achieve_tab")] public string? AchieveTab { get; set; }
    /// <summary>是否展示宝图次数</summary>
    [JsonProperty("treasure_times_publish")] public int? TreasureTimesPublish { get; set; }
    /// <summary>是否展示击杀次数</summary>
    [JsonProperty("kill_times_publish")] public int? KillTimesPublish { get; set; }
    /// <summary>是否展示新生水晶冲突排行</summary>
    [JsonProperty("newrank_publish")] public int? NewRankPublish { get; set; }
    /// <summary>是否展示水晶冲突段位</summary>
    [JsonProperty("crystal_rank_publish")] public int? CrystalRankPublish { get; set; }
    /// <summary>是否展示捕鱼次数</summary>
    [JsonProperty("fish_times_publish")] public int? FishTimesPublish { get; set; }
    /// <summary>崩之家徽章等级</summary>
    [JsonProperty("collapse_badge")] public int? CollapseBadge { get; set; }
    /// <summary>狒狒徽章等级</summary>
    [JsonProperty("feifei_badge")] public int? FeifeiBadge { get; set; }

    /// <summary>徽章信息，接口以 JSON 字符串形式返回</summary>
    [JsonProperty("badge")] public string? BadgeJson { get; set; }
    /// <summary>资料页展示的成就记录</summary>
    [JsonProperty("achieveInfo")] public IList<RisingStonesAchievement>? Achievements { get; set; }
    /// <summary>资料页展示的精选成就记录</summary>
    [JsonProperty("achieveTopInfo")] public IList<RisingStonesAchievement>? TopAchievements { get; set; }
    /// <summary>各职业等级记录</summary>
    [JsonProperty("careerLevel")] public IList<RisingStonesCareerLevel>? CareerLevels { get; set; }
    /// <summary>角色详情，部分字段可能根据隐私设置隐藏</summary>
    [JsonProperty("characterDetail")] public IList<RisingStonesCharacterDetail>? CharacterDetails { get; set; }

    /// <summary>关注数和粉丝数</summary>
    [JsonProperty("followFansiNum")] public RisingStonesFollowCounts? FollowCounts { get; set; }
    /// <summary>互动数量</summary>
    [JsonProperty("interactNum")] public string? InteractionCount { get; set; }
    /// <summary>收到的点赞数</summary>
    [JsonProperty("beLikedNum")] public string? LikeCount { get; set; }
    /// <summary>当前请求用户与该角色的关系代码</summary>
    [JsonProperty("relation")] public int? Relation { get; set; }
}

/// <summary>关注数和粉丝数</summary>
public sealed class RisingStonesFollowCounts
{
    /// <summary>关注人数</summary>
    [JsonProperty("followNum")] public int? Following { get; set; }
    /// <summary>粉丝人数</summary>
    [JsonProperty("fansNum")] public int? Fans { get; set; }
}

/// <summary>资料页中的成就或奖章记录</summary>
public sealed class RisingStonesAchievement
{
    /// <summary>奖章 ID</summary>
    [JsonProperty("medal_id")] public string? MedalId { get; set; }
    /// <summary>奖章类型</summary>
    [JsonProperty("medal_type")] public string? MedalType { get; set; }
    /// <summary>成就 ID</summary>
    [JsonProperty("achieve_id")] public string? AchievementId { get; set; }
    /// <summary>接口返回的带前缀成就 ID 字段</summary>
    [JsonProperty("a.achieve_id")] public string? QualifiedAchievementId { get; set; }
    /// <summary>成就获得时间</summary>
    [JsonProperty("achieve_time")] public string? AchievementTime { get; set; }
    /// <summary>服务器 ID</summary>
    [JsonProperty("group_id")] public string? ServerId { get; set; }
    /// <summary>角色名称</summary>
    [JsonProperty("character_name")] public string? CharacterName { get; set; }
    /// <summary>奖章类型 ID</summary>
    [JsonProperty("medal_type_id")] public string? MedalTypeId { get; set; }
    /// <summary>成就名称</summary>
    [JsonProperty("achieve_name")] public string? AchievementName { get; set; }
    /// <summary>大区 ID</summary>
    [JsonProperty("area_id")] public string? DataCenterId { get; set; }
    /// <summary>成就说明</summary>
    [JsonProperty("achieve_detail")] public string? AchievementDetail { get; set; }
    /// <summary>数据日期字段，具体含义尚未确认</summary>
    [JsonProperty("part_date")] public string? PartDate { get; set; }
}

/// <summary>一个职业的等级记录</summary>
public sealed class RisingStonesCareerLevel
{
    /// <summary>职业名称</summary>
    [JsonProperty("career")] public string? Career { get; set; }
    /// <summary>职业等级</summary>
    [JsonProperty("character_level")] public string? CharacterLevel { get; set; }
    /// <summary>数据日期字段，具体含义尚未确认</summary>
    [JsonProperty("part_date")] public string? PartDate { get; set; }
    /// <summary>数据更新时间</summary>
    [JsonProperty("update_date")] public string? UpdateDate { get; set; }
    /// <summary>职业分类</summary>
    [JsonProperty("career_type")] public string? CareerType { get; set; }
}

/// <summary>角色详情，部分字段可能根据隐私设置隐藏</summary>
public sealed class RisingStonesCharacterDetail
{
    /// <summary>角色创建时间</summary>
    [JsonProperty("create_time")] public string? CreateTime { get; set; }
    /// <summary>性别代码</summary>
    [JsonProperty("gender")] public string? Gender { get; set; }
    /// <summary>最后登录时间</summary>
    [JsonProperty("last_login_time")] public string? LastLoginTime { get; set; }
    /// <summary>种族代码</summary>
    [JsonProperty("race")] public string? Race { get; set; }
    /// <summary>角色名称</summary>
    [JsonProperty("character_name")] public string? CharacterName { get; set; }
    /// <summary>游戏角色 ID</summary>
    [JsonProperty("character_id")] public string? CharacterId { get; set; }
    /// <summary>大区 ID</summary>
    [JsonProperty("area_id")] public string? DataCenterId { get; set; }
    /// <summary>累计游玩时间</summary>
    [JsonProperty("play_time")] public string? PlayTime { get; set; }
    /// <summary>房屋信息</summary>
    [JsonProperty("house_info")] public string? HouseInfo { get; set; }
    /// <summary>服务器 ID</summary>
    [JsonProperty("group_id")] public string? ServerId { get; set; }
    /// <summary>部队名称</summary>
    [JsonProperty("guild_name")] public string? GuildName { get; set; }
    /// <summary>部队 ID</summary>
    [JsonProperty("fc_id")] public string? FreeCompanyId { get; set; }
    /// <summary>部族代码</summary>
    [JsonProperty("tribe")] public string? Tribe { get; set; }
    /// <summary>部队简写</summary>
    [JsonProperty("guild_tag")] public string? GuildTag { get; set; }
    /// <summary>洗澡次数</summary>
    [JsonProperty("washing_num")] public string? WashingCount { get; set; }
    /// <summary>数据日期字段，具体含义尚未确认</summary>
    [JsonProperty("part_date")] public string? PartDate { get; set; }
    /// <summary>宝图次数</summary>
    [JsonProperty("treasure_times")] public string? TreasureTimes { get; set; }
    /// <summary>击杀次数</summary>
    [JsonProperty("kill_times")] public string? KillTimes { get; set; }
    /// <summary>新排行数据</summary>
    [JsonProperty("newrank")] public string? NewRank { get; set; }
    /// <summary>水晶冲突段位</summary>
    [JsonProperty("crystal_rank")] public string? CrystalRank { get; set; }
    /// <summary>捕鱼次数</summary>
    [JsonProperty("fish_times")] public string? FishTimes { get; set; }
}
