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
    /// 发布帖子
    /// </summary>
    /// <param name="title,content,field"></param>
    [HttpPost("push")]
    [Authorize]
    public async Task<ActionResult<ResponseResult<Post>>> PostPost([FromBody] string title, [FromBody] string content,[FromBody] string field, [FromHeader(Name = "Authorization")] string token)
    {
        
        ResponseResult<Post> responseResult = await _postService.PublishPost(title, content, field, token);
        return CreatedAtAction(nameof(PostPost), responseResult);
    }

    /// <summary>
    /// 删除帖子
    /// </summary>
    /// <param name="postId"></param>
    [HttpDelete("remove")]
    [Authorize]
    public async Task<ActionResult<ResponseResult<Post?>>> DeletePost([FromBody] int PostId, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<Post?> responseResult=await _postService.DeletePost(PostId, token);
        return CreatedAtAction(nameof(DeletePost), responseResult);
    }

    /// <summary>
    /// 取消点赞
    /// </summary>
    /// <param name="replyId"></param>
    [HttpDelete("cancelLike")]
    [Authorize]
    public async Task<ActionResult<ResponseResult<Reply?>>> CancelLike([FromBody] int ReplyId, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<Reply?> responseResult = await _postService.CancelLike(ReplyId, token);
        return CreatedAtAction(nameof(CancelLike), responseResult);
    }

    /// <summary>
    /// 获取用户发帖历史
    /// </summary>
    /// <param name="userId"></param>
    [HttpGet("history")]
    [Authorize]
    public async Task<ActionResult<ResponseResult<List<Post>>>> GetUsersAllPosts([FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<List<Post>> responseResult = await _postService.GetUsersAllPosts(token);
        return CreatedAtAction(nameof(GetUsersAllPosts), responseResult);
    }

    /// <summary>
    /// 获取十个对应领域的帖子
    /// </summary>
    /// <param name="field"></param>
    [HttpGet("getPostsByField")]
    [Authorize]
    public async Task<ActionResult<ResponseResult<List<Post>>>> GetTenPostsByField([FromQuery] string field, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<List<Post>> responseResult = await _postService.GetTenPostsByField(field, token);
        return CreatedAtAction(nameof(GetTenPostsByField), responseResult);
    }

    /// <summary>
    /// 获取全部帖子
    /// </summary>
    /// <param></param>
    [HttpGet("allPosts")]
    [Authorize]
    public async Task<ActionResult<ResponseResult<List<Post>>>> GetAllPosts([FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<List<Post>> responseResult = await _postService.GetAllPosts(token);
        return CreatedAtAction(nameof(GetAllPosts), responseResult);
    }

    /// <summary>
    /// 获取该领域全部帖子
    /// </summary>
    /// <param></param>
    [HttpGet("getAllPostsByField")]
    [Authorize]
    public async Task<ActionResult<ResponseResult<List<Post>>>> GetAllPostsByField([FromQuery] string field, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<List<Post>> responseResult = await _postService.GetAllPostsByField(field, token);
        return CreatedAtAction(nameof(GetAllPostsByField), responseResult);
    }
}