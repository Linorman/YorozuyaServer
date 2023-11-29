using Newtonsoft.Json;

namespace YorozuyaServer.entity;

public partial class Image
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("imageUrl")]
    public string ImageUrl { get; set; }

    [JsonProperty("postId")]
    public long PostId { get; set; }
}