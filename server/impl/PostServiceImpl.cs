using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
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
    
    public async Task<ResponseResult<Reply?>> PublishReply(int postId, string content, string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(userId);
        if (userInfo == null)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
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
    
    public async Task<ResponseResult<Reply?>> DeleteReply(int replyId, string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(userId);
        if (userInfo == null)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        Reply? reply = await _dbContext.Replies.FindAsync(replyId);
        if (reply == null)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.REPLY_NOT_EXIST, null);
        }
        _dbContext.Replies.Remove(reply);
        await _dbContext.SaveChangesAsync();
        return ResponseResult<Reply?>.Success(ResultCode.REPLY_DELETE_SUCCESS, reply);
    }
    
    public async Task<ResponseResult<List<Reply>>> GetAllReply(string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(userId);
        if (userInfo == null)
        {
            return ResponseResult<List<Reply>>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        List<Reply> replies = await _dbContext.Replies.ToListAsync();
        if (replies.Count == 0)
        {
            return ResponseResult<List<Reply>>.Fail(ResultCode.GET_ALL_REPLY_FAIL, null);
        }
        return ResponseResult<List<Reply>>.Success(ResultCode.GET_ALL_REPLY_SUCCESS, replies);
    }
}