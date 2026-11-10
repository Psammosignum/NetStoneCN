using NetStoneCN.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;

namespace NetStoneCN;

/// <summary>用于获取石之家角色资料的客户端</summary>
public sealed class RisingStonesClient : IDisposable
{
    private const string UserInfoPath = $"{Constants.ApiBaseAddress}api/home/userInfo/getUserInfo";
    private const string BindUserInfoPath = $"{Constants.ApiBaseAddress}api/home/groupAndRole/getCharacterBindInfo?platform=2";
    private const string JobConfigListPath = $"{Constants.ApiBaseAddress}api/home/recruit/getJobConfigList";
    private const string CharacterSearchPath = $"{Constants.ApiBaseAddress}api/common/search";
    private readonly HttpClient client;
    private readonly bool ownsClient;
    private string cookieHeader = string.Empty;
    /// <summary>初始化</summary>
    public RisingStonesClient() : this(CreateHttpClient(), true) { }

    /// <summary>创建http实例</summary>
    public RisingStonesClient(HttpClient httpClient) : this(httpClient, false) { cookieHeader = string.Empty; }

    /// <summary>设置后续请求使用的石之家 Cookie 请求头</summary>
    public void SetCookie(string cookies)
    {
        if (string.IsNullOrWhiteSpace(cookies))
            throw new ArgumentException("必须提供石之家 Cookie。", nameof(cookies));
        Volatile.Write(ref cookieHeader, cookies.Trim());
    }

    private RisingStonesClient(HttpClient httpClient, bool ownsClient)
    {
        client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.ownsClient = ownsClient;
        client.BaseAddress ??= new Uri(Constants.ApiBaseAddress);
        if (!client.DefaultRequestHeaders.Accept.Any())
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json, text/plain, */*");
        client.DefaultRequestHeaders.Referrer ??= new Uri(Constants.WebBaseAddress);
        if (!client.DefaultRequestHeaders.Contains("Origin"))
            client.DefaultRequestHeaders.TryAddWithoutValidation(
                "Origin", new Uri(Constants.WebBaseAddress).GetLeftPart(UriPartial.Authority));
    }

    /// <summary>根据石之家角色 UUID 获取角色资料</summary>
    public Task<RisingStonesCharacter?> GetCharacter(string uuid, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(uuid)) throw new ArgumentException("必须提供角色 UUID。", nameof(uuid));
        return GetCharacterViaURL($"{UserInfoPath}?uuid={Uri.EscapeDataString(uuid.Trim())}", cancellationToken);
    }

    /// <summary>获取当前登录账号绑定的角色资料</summary>
    public Task<CurrentCharacter?> GetCurrentCharacter(CancellationToken cancellationToken = default) =>
        GetDataViaURL<CurrentCharacter>(BindUserInfoPath, cancellationToken);

    /// <summary>获取招募页面使用的职业配置列表</summary>
    public Task<JobConfigList?> GetJobConfigList(CancellationToken cancellationToken = default) =>
        GetDataViaURL<JobConfigList>(JobConfigListPath, cancellationToken);

    /// <summary>按关键词搜索角色，并按大区和服务器筛选；仅唯一匹配时返回角色 UUID</summary>
    /// <param name="keyword">搜索关键词</param>
    /// <param name="dataCenter">可选大区名称筛选</param>
    /// <param name="server">可选服务器名称筛选</param>
    /// <param name="page">页码，从 1 开始</param>
    /// <param name="limit">每页数量，范围为 1 到 15</param>
    /// <param name="cancellationToken">用于取消请求的令牌</param>
    /// <returns>恰好匹配一名角色时返回其 UUID；零个或多个匹配时返回 null</returns>
    public async Task<string?> SearchCharacters(
        string keyword,
        string? dataCenter = null,
        string? server = null,
        int page = 1,
        int limit = 15,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(keyword)) throw new ArgumentException("必须提供搜索关键词。", nameof(keyword));
        if (page < 1) throw new ArgumentOutOfRangeException(nameof(page), "页码必须大于或等于 1。");
        if (limit is < 1 or > 15) throw new ArgumentOutOfRangeException(nameof(limit), "每页数量必须在 1 到 15 之间。");
        var query = $"type=6&keywords={Uri.EscapeDataString(keyword.Trim())}" +
                    $"&orderBy=comment&page={page}&limit={limit}";
        var results = await GetDataViaURL<List<RisingStoneCharacterSearch>>(
            $"{CharacterSearchPath}?{query}", cancellationToken).ConfigureAwait(false);
        if (results is null) return null;

        var matches = results.Where(character =>
                (string.IsNullOrWhiteSpace(dataCenter) || string.Equals(character.DataCenterName?.Trim(), dataCenter.Trim(), StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(server) || string.Equals(character.ServerName?.Trim(), server.Trim(), StringComparison.OrdinalIgnoreCase)))
            .ToList();

        return matches.Count == 1 ? matches[0].Uuid : null;
    }

    private async Task<RisingStonesCharacter?> GetCharacterViaURL(string requestUri, CancellationToken cancellationToken)
        => await GetDataViaURL<RisingStonesCharacter>(requestUri, cancellationToken).ConfigureAwait(false);

    private async Task<T?> GetDataViaURL<T>(string requestUri, CancellationToken cancellationToken)
    {
        var cookies = Volatile.Read(ref cookieHeader);
        if (string.IsNullOrWhiteSpace(cookies))
            throw new InvalidOperationException("尚未设置石之家 Cookie，请先调用 SetCookie。");
        using var request = InitRequest(requestUri, cookies);
        using var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"石之家返回 HTTP {(int)response.StatusCode}（{response.StatusCode}）。");

        var json = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(json))
        {
            var contentType = response.Content.Headers.ContentType?.ToString() ?? "未提供";
            var contentLength = response.Content.Headers.ContentLength?.ToString() ?? "未提供";
            throw new RisingStonesException(
                $"石之家返回了空响应正文（HTTP {(int)response.StatusCode}，Content-Type: {contentType}，Content-Length: {contentLength}）。");
        }

        RisingStonesResponse<T>? result;
        var envelope = JsonConvert.DeserializeObject<RisingStonesResponse<JToken>>(json);
        if (envelope is null) throw new RisingStonesException("石之家返回内容为空。");
        if (envelope.Code != 10000) throw new RisingStonesException(envelope.Message ?? "石之家请求失败。", envelope.Code);
        try { result = JsonConvert.DeserializeObject<RisingStonesResponse<T>>(json); }
        catch (JsonException ex) { throw new RisingStonesException("石之家返回的 JSON 格式不正确。", innerException: ex); }

        if (result is null) throw new RisingStonesException("石之家返回内容为空。");
        
        return result.Data;
    }

    private static HttpRequestMessage InitRequest(string targetUri, string cookies)
    {
        if (cookies is null) throw new ArgumentNullException(nameof(cookies));
        var request = new HttpRequestMessage(HttpMethod.Get, targetUri)
        {
            Version = HttpVersion.Version20,
            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower,
        };
        request.Headers.TryAddWithoutValidation("Cookie", cookies);
        request.Headers.Accept.ParseAdd("application/json, text/plain, */*");
        request.Headers.Referrer = new Uri(Constants.WebBaseAddress);
        request.Headers.TryAddWithoutValidation("Origin", Constants.WebBaseAddress);
        request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/157.0.0.0 Safari/537.36");
        request.Headers.TryAddWithoutValidation("Accept-Language", "zh-CN;q=0.9,zh;q=0.8");
        return request;
    }
    private static HttpClient CreateHttpClient()
    {
       
        var handler = new SocketsHttpHandler
        {
            UseCookies = false,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,
            PooledConnectionLifetime = TimeSpan.Zero,
        };
        
        return new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(20) };
    }

    /// <summary>释放客户端创建的 HTTP 资源；外部传入的 HttpClient 由调用方管理</summary>
    public void Dispose() { if (ownsClient) client.Dispose(); }
}
