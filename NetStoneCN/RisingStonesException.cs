namespace NetStoneCN;

/// <summary>Represents an unsuccessful Rising Stones API response.</summary>
public sealed class RisingStonesException : Exception
{
    /// <summary>石之家返回的状态码，如有的话</summary>
    public int? ApiCode { get; }

    /// <summary>Creates an exception for an API or response error.</summary>
    public RisingStonesException(string message, int? apiCode = null, Exception? innerException = null)
        : base(message, innerException) => ApiCode = apiCode;
}
