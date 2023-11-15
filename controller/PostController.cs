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
    public async Task<ActionResult<ResponseResult<Reply?>>> PublishReply([FromBody] int postId, [FromBody] string content, [FromHeader(Name = "Authorization")] string token)
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
    public async Task<ActionResult<ResponseResult<Reply?>>> DeleteReply([FromBody] int replyId, [FromHeader(Name = "Authorization")] string token)
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
    
    /// <summary>
    /// 获取帖子回复
    /// </summary>
    /// <param></param>
    [Authorize]
    [HttpGet("postReplies")]
    public async Task<ActionResult<ResponseResult<List<Reply>>>> GetPostReply([FromBody] int postId, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<List<Reply>> responseResult = await _postService.GetPostReply(postId, token);
        return CreatedAtAction(nameof(GetPostReply), responseResult);
    }
    
    /// <summary>
    /// 采纳回复
    /// </summary>
    /// <param></param>
    [Authorize]
    [HttpPost("acceptReply")]
    public async Task<ActionResult<ResponseResult<Reply?>>> AcceptReply([FromBody] int replyId, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<Reply?> responseResult = await _postService.AcceptReply(replyId, token);
        return CreatedAtAction(nameof(AcceptReply), responseResult);
    }
    
    /// <summary>
    /// 点赞
    /// </summary>
    /// <param></param>
    [Authorize]
    [HttpPost("like")]
    public async Task<ActionResult<ResponseResult<Reply?>>> LikeReply([FromBody] int replyId, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<Reply?> responseResult = await _postService.LikeReply(replyId, token);
        return CreatedAtAction(nameof(LikeReply), responseResult);
    }
    
    /// <summary>
    /// 获取是否点赞
    /// </summary>
    /// <param></param>
    [Authorize]
    [HttpGet("isLiked")]
    public async Task<ActionResult<ResponseResult<Int32?>>> GetLikeStatus([FromBody] int replyId, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<Int32?> responseResult = await _postService.GetLikeStatus(replyId, token);
        return CreatedAtAction(nameof(GetLikeStatus), responseResult);
    }
}