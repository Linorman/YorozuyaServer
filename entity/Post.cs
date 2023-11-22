using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YorozuyaServer.entity;

public partial class Post
{
    [JsonProperty("askerId")]
    public long AskerId { get; set; }

    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("createTime")]
    public string CreateTime { get; set; }

    [JsonProperty("delTag")]
    public long DelTag { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("updateTime")]
    public string UpdateTime { get; set; }

    [JsonProperty("views")]
    public long Views { get; set; }

    [JsonProperty("field")]
    public string Field  { get; set; }
}


