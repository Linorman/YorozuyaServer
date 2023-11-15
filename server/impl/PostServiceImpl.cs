using YorozuyaServer.common;
using YorozuyaServer.config;
using YorozuyaServer.entity;
using YorozuyaServer.utils;

namespace YorozuyaServer.server.impl;

public class PostServiceImpl : PostService
{
    private readonly DbConfig _dbContext;
    private readonly RedisUtil _redisUtil;
    
    public PostServiceImpl(DbConfig dbContext, RedisUtil redisUtil)
    {
        _dbContext = dbContext;
        _redisUtil = redisUtil;
    }
    
    public async Task<ResponseResult<Reply?>> PublishReply(int postId, string content)
    {
        Reply reply = new Reply
        {
            PostId = postId,
            Content = content,
            CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
        _dbContext.Replies.Add(reply);
        await _dbContext.SaveChangesAsync();
        return ResponseResult<Reply?>.Success(ResultCode.REPLY_PUBLISH_SUCCESS, reply);
    }
    
    
}