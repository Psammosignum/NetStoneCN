# NetStoneCN

NetStoneCN 是一个用于调用最终幻想 XIV 国服社区网站“石之家”接口的 .NET 类库。项目参考了 [NetStone](https://github.com/xivapi/NetStone/tree/master) 的调用风格与组织方式，为石之家角色资料、角色搜索、当前登录角色和职业配置提供类型化的数据模型。

项目使用 AI 辅助编写，代码由维护者审阅编辑。（这个文档也是写完让ai写然后再改的（目移））接口属于石之家网站的非公开 Web API，字段、认证要求及行为可能随网站更新而变化。

## 功能

- 使用调用方提供的石之家登录 Cookie 发送 API 请求。
- 按角色 UUID 获取角色资料与头像链接。
- 按关键词搜索角色，并可按大区和服务器名称筛选；只有唯一匹配时返回 UUID。
- 获取当前登录账号绑定的角色及基础资料。
- 获取招募页面的职业/职能图标与名称配置。
- 通过 `CancellationToken` 支持取消网络请求。

## 环境要求

当前库面向 .NET 8.0。使用方需运行于兼容 .NET 8 的环境，并能访问石之家 API。

## 安装与基本用法

将 NetStoneCN 项目引用到应用，或使用项目生成的 NuGet 包。基本调用如下：

```csharp
using NetStoneCN;

using var client = new RisingStoneClient();
client.SetCookie(cookieHeader);
```

`cookieHeader` 是浏览器请求头中的 Cookie 值，不含 `Cookie:` 前缀，格式为 `名称=值; 名称2=值2`。Cookie 是可复用的登录凭据，请从本机安全存储读取，不要硬编码到源码、提交到版本控制或写入日志。Cookie 过期后需重新登录并更新。

默认情况下，客户端会使用库内置的 User-Agent。若服务端要求请求使用与登录浏览器相同的 User-Agent，可通过 `SetUA` 设置；此设置只影响该客户端后续发出的请求。传入 `null` 或空白字符串会恢复默认 User-Agent：

```csharp
client.SetUA(browserUserAgent);
// 恢复库默认值：client.SetUA(null);
```

`browserUserAgent` 应是浏览器的完整 User-Agent 字符串（不含 `User-Agent:` 前缀）。例如使用 WebView2 时，可从 `CoreWebView2.Settings.UserAgent` 获取。User-Agent 与 Cookie 应来自同一登录会话对应的浏览器环境；不要把 Cookie 或其他认证信息写入日志。

## 按 UUID 获取角色资料

```csharp
var character = await client.GetCharacter("10001019");//示例使用石之家小助手
if (character is not null)
{
    Console.WriteLine($"{character.Name} - {character.DataCenterName}/{character.ServerName}");
    Console.WriteLine(character.Avatar);
}
```

UUID 是石之家角色 ID。例如，个人资料地址 `...#/me/info?uuid=10001019` 中的 `uuid` 查询值可用于调用。也可以先用 `SearchCharacters` 按名称搜索。

`RisingStonesCharacter` 包含角色标识与名称、大区/服务器 ID 和名称、头像（返回链接，可以使用在<img src="...">中，无头像返回为null，无头像情况下石之家直接返回静态图片，有需要自行设置判断与返回逻辑）、简介、公开设置、徽章、职业等级、成就、角色详情、关注/粉丝数及互动数等字段。具体属性及 JSON 对应关系请查看 `NetStoneCN/Model/RisingStonesCharacter.cs`。接口可能按用户隐私设置省略或返回空字段；头像为空时由应用显示默认图片。

## 搜索角色

```csharp
var uuid = await client.SearchCharacters(
    keyword: "石之家小助手",
    dataCenter: "莫古力",
    server: "拂晓之间");

if (uuid is not null)
    Console.WriteLine($"匹配到角色 UUID：{uuid}");
else
    Console.WriteLine("没有唯一匹配的角色。");
```

方法使用石之家角色搜索接口，关键词会进行 URL 编码。`dataCenter`、`server` 是在返回结果上按大区名和服务器名进行的本地筛选参数；筛选后恰好一条记录时返回其 UUID，零条或多条时返回 `null`。默认每页 15 条，`limit` 不可大于 15；若搜索结果超过一页，可调整 `page`。

## 获取当前登录角色

```csharp
var current = await client.GetCurrentCharacter();
if (current is not null)
{
    Console.WriteLine($"{current.Name} - {current.DataCenterName}/{current.ServerName}");
    Console.WriteLine($"头像：{current.Avatar}");
}
```

`CurrentCharacter` 描述当前登录账号绑定的角色及接口提供的基础资料，例如角色名、大区、服务器、头像、简介、公开设置和角色详情。敏感字段不会由库额外请求或推导；数据是否存在也受接口及账号公开设置影响。模型定义见 `NetStoneCN/Model/CurrentCharacter.cs`。

## 获取登录状态和简易账号信息

`GetLoginInformation` 返回当前登录账号的简易信息，包括角色名、大区和服务器名称：

```csharp
var loginInfo = await client.GetLoginInformation();
if (loginInfo is not null)
{
    Console.WriteLine($"{loginInfo.Name} - {loginInfo.DataCenterName}/{loginInfo.ServerName}");
}
```

`IsLoggedIn` 用于检查当前 Cookie 是否能通过石之家登录状态接口验证，结果为 `true` 或 `false`：

```csharp
var isLoggedIn = await client.IsLoggedIn();
Console.WriteLine(isLoggedIn ? "已登录" : "未登录");
```

两个方法都支持传入 `CancellationToken`。`LoginInformation` 模型定义见 `NetStoneCN/Model/LoginInformation.cs`。

## 获取职业与职能配置

```csharp
var jobs = await client.GetJobConfigList();
if (jobs is not null && jobs.TryGetValue("防护职业", out var tanks))
{
    foreach (var job in tanks)
        Console.WriteLine($"{job.Value}: {job.JobPicUrl}");
}
```

返回值 `JobConfigList` 是以分类名称为键的集合，包含职能分类和具体职业条目；每项提供编号、名称、图标 URL 与分类名称。接口当前返回完整配置，库不额外筛选。图标 URL 可供网页 `<img src="...">` 或桌面 UI 使用。模型定义见 `NetStoneCN/Model/JobConfigList.cs`。

## 取消请求与异常处理

网络方法均可传入 `CancellationToken`。例如：

```csharp
using var cancellation = new CancellationTokenSource(TimeSpan.FromSeconds(10));
var character = await client.GetCharacter("10001019", cancellation.Token);
```

取消令牌可在用户取消操作或超时后停止等待/取消网络请求。请求失败时可能抛出 `HttpRequestException`；API 返回非成功业务码时抛出 `RisingStonesException`，可读取其 `ApiCode`。不要在错误日志中输出 Cookie 或认证请求头。

## 项目结构

- `NetStoneCN/RisingStonesClient.cs`：客户端、请求构造、搜索筛选与 API 调用。
- `NetStoneCN/Model/RisingStonesCharacter.cs`：角色资料模型及关联模型。
- `NetStoneCN/Model/RisingStoneCharacterSearch.cs`：角色搜索结果模型。
- `NetStoneCN/Model/CurrentCharacter.cs`：当前登录角色模型。
- `NetStoneCN/Model/JobConfigList.cs`：职业配置模型。
- `NetStoneCN/Model/RisingStonesResponse.cs`：通用响应信封。
- `NetStoneCN/RisingStonesException.cs`：石之家业务异常。

## 注意事项

- 本项目不负责登录或刷新登录 Cookie；调用方需自行通过石之家网页登录并安全提供有效 Cookie。
- Cookie 等同于登录凭据。避免分享、提交、记录或在异常信息中输出其内容；泄露后应退出登录或使会话失效。
- 接口属于网站实现细节，可能限流、变更或暂时不可用。请合理控制请求频率，并遵守石之家网站规则。
- 数据字段可能为空、缺失或随接口版本改变；调用方应按可空值处理。
