using YorozuyaServer.common;
using YorozuyaServer.entity;

namespace YorozuyaServer.service;

public interface PostService
{
    /// <summary>
    /// 发布回复
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Reply?>> PublishReply(int postId, string content, string token);
    
    /// <summary>
    /// 删除回复
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Reply?>> DeleteReply(int replyId, string token);
    
    /// <summary>
    /// 获取全部回复
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Reply>>> GetAllReply(string token);
    
    /// <summary>
    /// 获取用户全部回复
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Reply>>> GetUsersAllReply(string token);
    
    /// <summary>
    /// 获取帖子回复
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Reply>>> GetPostReply(int postId);
    
    /// <summary>
    /// 采纳回复
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Reply?>> AcceptReply(int replyId, string token);
    
    /// <summary>
    /// 点赞回复
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Reply?>> LikeReply(int replyId, string token);
    
    /// <summary>
    /// 获取点赞状态
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Int32?>> GetLikeStatus(int replyId, string token);

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
    Task<ResponseResult<List<Post>>> GetTenPostsByField(string field);

    /// <summary>
    /// 获取全部帖子
    /// </summary>
    /// <param name="Post"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Post>>> GetAllPosts();

    /// <summary>
    /// 获取该领域全部帖子
    /// </summary>
    /// <param name="Post"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Post>>> GetAllPostsByField(string field);

    /// <summary>
    /// 根据postId获取帖子
    /// </summary>
    /// <param name="Post"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Post>>> GetPostByPostId(int postId);

    /// <summary>
    /// 根据title获取帖子
    /// </summary>
    /// <param name="Post"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<List<Post>>> GetPostByTitle(string title);
}