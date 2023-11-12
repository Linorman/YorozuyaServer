using Newtonsoft.Json;

namespace YorozuyaServer.entity;

public partial class Reply
{
    [JsonProperty("content")]
    public string Content { get; set; }

    [JsonProperty("createTime")]
    public string CreateTime { get; set; }

    [JsonProperty("delTag")]
    public long DelTag { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("isAccepted")]
    public bool IsAccepted { get; set; }

    [JsonProperty("likes")]
    public long Likes { get; set; }

    [JsonProperty("postId")]
    public long PostId { get; set; }

    [JsonProperty("updateTime")]
    public string UpdateTime { get; set; }

    [JsonProperty("userId")]
    public long UserId { get; set; }
}