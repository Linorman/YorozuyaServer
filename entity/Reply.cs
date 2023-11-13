using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace YorozuyaServer.entity;

public partial class Reply
{
    [JsonProperty("content")]
    [Required]
    public string Content { get; set; }

    [JsonProperty("createTime")]
    public string CreateTime { get; set; }

    //É¾³ý±êÊ¶
    [JsonProperty("delTag")]
    public long DelTag { get; set; }

    [JsonProperty("id")]
    [Key]
    public long Id { get; set; }

    [JsonProperty("isAccepted")]
    [Required]
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