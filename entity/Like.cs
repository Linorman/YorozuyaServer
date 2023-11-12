using Newtonsoft.Json;

namespace YorozuyaServer.entity;

public partial class Like
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("replyId")]
    public long ReplyId { get; set; }

    [JsonProperty("userId")]
    public long UserId { get; set; }
}