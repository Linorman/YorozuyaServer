using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YorozuyaServer.entity;

public partial class Post
{
    [JsonProperty("askerId")]
    [Required]
    public long AskerId { get; set; }

    [JsonProperty("content")]
    [Required]
    public string Content { get; set; }

    [JsonProperty("createTime")]
    [Required]
    public string CreateTime { get; set; }

    //É¾³ý±êÊ¶
    [JsonProperty("delTag")]
    public long DelTag { get; set; }

    [JsonProperty("id")]
    [Key]
    public long Id { get; set; }

    [JsonProperty("title")]
    [Required]  
    public string Title { get; set; }

    [JsonProperty("updateTime")]
    public string UpdateTime { get; set; }

    [JsonProperty("field")]
    [Required]
    public string Field {  get; set; }

    [JsonProperty("views")]
    public long Views { get; set; }

    public static implicit operator Post(Post v)
    {
        throw new NotImplementedException();
    }
}


