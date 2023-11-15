using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YorozuyaServer.common;
using YorozuyaServer.entity;
using YorozuyaServer.server;

namespace YorozuyaServer.controller;

[Microsoft.AspNetCore.Components.Route("api/post")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly PostService _postService;
    
    public PostController(PostService postService)
    {
        _postService = postService;
    }
    
    /// <summary>
    /// 用户回复
    /// </summary>
    /// <param name="postId, content"></param>
    [Authorize]
    [HttpPost("reply")]
    public async Task<ActionResult<ResponseResult<Reply?>>> PublishReply([FromForm] int postId, [FromForm] string content, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<Reply?> responseResult = await _postService.PublishReply(postId, content, token);
        return CreatedAtAction(nameof(PublishReply), responseResult);
    }
    
    /// <summary>
    /// 删除回复
    /// </summary>
    /// <param name="replyId"></param>
    [Authorize]
    [HttpDelete("deleteReply")]
    public async Task<ActionResult<ResponseResult<Reply?>>> DeleteReply([FromForm] int replyId, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<Reply?> responseResult = await _postService.DeleteReply(replyId, token);
        return CreatedAtAction(nameof(DeleteReply), responseResult);
    }
    
    /// <summary>
    /// 获取全部回复
    /// </summary>
    /// <param></param>
    [Authorize]
    [HttpGet("allReplies")]
    public async Task<ActionResult<ResponseResult<List<Reply>>>> GetAllReply([FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<List<Reply>> responseResult = await _postService.GetAllReply(token);
        return CreatedAtAction(nameof(GetAllReply), responseResult);
    }
}