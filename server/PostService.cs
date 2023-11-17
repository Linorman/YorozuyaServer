using YorozuyaServer.common;
using YorozuyaServer.entity;

namespace YorozuyaServer.server;

public interface PostService
{
    /// <summary>
    /// 发布回复
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Reply?>> PublishReply(int postId, string content);

    /// <summary>
    /// 发布帖子
    /// </summary>
    /// <param name="post"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Post>> PublishPost(string title, string content, string field, string token);

    /// <summary>
    /// 删除帖子
    /// </summary>
    /// <param name="post"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Post?>> DeletePost(int postId, string token);

    /// <summary>
    /// 取消点赞
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Reply?>> CancelLike(int replyId,string token);

    /// <summary>
    /// 获取用户发帖历史
    /// </summary>
    /// <param name="Post"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Post>>> GetUsersAllPosts(string token);

    /// <summary>
    /// 获取十个对应领域的帖子
    /// </summary>
    /// <param name="Post"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Post>>> GetTenPostsByField(string field, string token);

    /// <summary>
    /// 获取全部帖子
    /// </summary>
    /// <param name="Post"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Post>>> GetAllPosts(string token);

    /// <summary>
    /// 获取该领域全部帖子
    /// </summary>
    /// <param name="Post"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Post>>> GetAllPostsByField(string field, string token);
}