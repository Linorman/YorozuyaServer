using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YorozuyaServer.entity;

public class UserInfo
{
    [JsonProperty("createTime")]
    public string CreateTime { get; set; }

    [JsonProperty("field")]
    public string Field { get; set; }

    [JsonProperty("gender")]
    public int Gender { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    /// <summary>
    /// 1是admin
    /// </summary>
    [JsonProperty("role")]
    public long Role { get; set; }

    [JsonProperty("updateTime")]
    public string UpdateTime { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; }
}