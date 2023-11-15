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

}