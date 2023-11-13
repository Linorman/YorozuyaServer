using System.ComponentModel.DataAnnotations;
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
    [Key]
    public long Id { get; set; }

    [JsonProperty("password")]
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// 1是admin
    /// </summary>
    [JsonProperty("role")]
    [Required]
    public long Role { get; set; }

    [JsonProperty("updateTime")]
    public string UpdateTime { get; set; }

    [JsonProperty("username")]
    [Required]
    public string Username { get; set; }
}