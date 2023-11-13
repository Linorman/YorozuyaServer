using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace YorozuyaServer.entity;

public partial class Like
{
    [JsonProperty("id")]
    [Key]
    public long Id { get; set; }

    [JsonProperty("replyId")]
    [Required]
    public long ReplyId { get; set; }

    [JsonProperty("userId")]
    [Required]
    public long UserId { get; set; }
}