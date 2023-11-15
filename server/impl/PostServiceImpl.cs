using Microsoft.EntityFrameworkCore;
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
    
    public async Task<ResponseResult<List<Reply>>> GetPostReply(int postId, string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(userId);
        if (userInfo == null)
        {
            return ResponseResult<List<Reply>>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        List<Reply> replies = await _dbContext.Replies.Where(reply => reply.PostId == postId).ToListAsync();
        if (replies.Count == 0)
        {
            return ResponseResult<List<Reply>>.Fail(ResultCode.GET_POST_REPLY_FAIL, null);
        }
        return ResponseResult<List<Reply>>.Success(ResultCode.GET_POST_REPLY_SUCCESS, replies);
    }
    
    public async Task<ResponseResult<Reply?>> AcceptReply(int replyId, string token)
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
        Post? post = await _dbContext.Posts.FindAsync(reply.PostId);
        if (userInfo.Id != post.AskerId)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.ACCEPT_REPLY_FAIL, null);
        }
        reply.IsAccepted = 1;
        _dbContext.Replies.Update(reply);
        await _dbContext.SaveChangesAsync();
        return ResponseResult<Reply?>.Success(ResultCode.ACCEPT_REPLY_SUCCESS, reply);
    }
    
    public async Task<ResponseResult<Reply?>> LikeReply(int replyId, string token)
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
        reply.Likes += 1;
        Like like = new Like
        {
            UserId = userId,
            ReplyId = replyId
        };
        _dbContext.Likes.Add(like);
        _dbContext.Replies.Update(reply);
        await _dbContext.SaveChangesAsync();
        return ResponseResult<Reply?>.Success(ResultCode.REPLY_LIKE_SUCCESS, reply);
    }

    public async Task<ResponseResult<Int32?>> GetLikeStatus(int replyId, string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(userId);
        if (userInfo == null)
        {
            return ResponseResult<Int32?>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        
        Like? like = await _dbContext.Likes.FirstOrDefaultAsync(like => like.UserId == userId && like.ReplyId == replyId);
        if (like == null)
        {
            return ResponseResult<Int32?>.Success(ResultCode.REPLY_LIKE_ALREADY_SUCCESS, 0);
        }
        return ResponseResult<Int32?>.Success(ResultCode.REPLY_LIKE_ALREADY_SUCCESS, 1);
    }
}