using YorozuyaServer.common;
using Microsoft.EntityFrameworkCore;
using YorozuyaServer.config;
using YorozuyaServer.entity;
using YorozuyaServer.utils;

namespace YorozuyaServer.service.impl;

public class PostServiceImpl : PostService
{
    private readonly DbConfig _dbContext;
    private readonly RedisUtil _redisUtil;
    
    public PostServiceImpl(DbConfig dbContext, RedisUtil redisUtil)
    {
        _dbContext = dbContext;
        _redisUtil = redisUtil;
    }

    public async Task<ResponseResult<Post>> PublishPost(string title, string content, string field, string token)
    {
        token = token[7..];
        Int32 askerId = Int32.Parse(_redisUtil.GetKey(token)!);

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)askerId);
        if (userInfo == null)
        {
            return ResponseResult<Post>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        Post post = new Post
        {
            AskerId = askerId,
            CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            DelTag = 0,
            Title = title,
            Content = content,
            Field = field
        };
        _dbContext.Posts.Add(post);
        await _dbContext.SaveChangesAsync();
        return ResponseResult<Post>.Success(ResultCode.POST_PUBLISH_SUCCESS, post);
    }

    public async Task<ResponseResult<Post?>> DeletePost(int postId, string token)
    {
        token = token[7..];
        Int32 askerId = Int32.Parse(_redisUtil.GetKey(token)!);

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)askerId);
        if (userInfo == null)
        {
            return ResponseResult<Post?>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        Post? post = await _dbContext.Posts.FindAsync((long)postId);

        if (post == null)
        {
            return ResponseResult<Post?>.Fail(ResultCode.POST_NOT_EXIST, null);
        }
        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync();
        return ResponseResult<Post?>.Success(ResultCode.POST_DELETE_SUCCESS, post);
    }

    
    public async Task<ResponseResult<Reply?>> PublishReply(int postId, string content, string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
        if (userInfo == null)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        Reply reply = new Reply
        {
            PostId = postId,
            UserId = userId,
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
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
        if (userInfo == null)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        Reply? reply = await _dbContext.Replies.FindAsync((long)replyId);
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
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
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
    
    public async Task<ResponseResult<List<Reply>>> GetUsersAllReply(string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
        if (userInfo == null)
        {
            return ResponseResult<List<Reply>>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        List<Reply> replies = await _dbContext.Replies.Where(reply => reply.UserId == userId).ToListAsync();
        if (replies.Count == 0)
        {
            return ResponseResult<List<Reply>>.Fail(ResultCode.GET_ALL_REPLY_FAIL, null);
        }
        return ResponseResult<List<Reply>>.Success(ResultCode.GET_ALL_REPLY_SUCCESS, replies);
    }
    
    public async Task<ResponseResult<List<Reply>>> GetPostReply(int postId)
    {
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
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
        if (userInfo == null)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        Reply? reply = await _dbContext.Replies.FindAsync((long)replyId);
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
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
        if (userInfo == null)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        Reply? reply = await _dbContext.Replies.FindAsync((long)replyId);
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
        
        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
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

    public async Task<ResponseResult<Reply?>> CancelLike(int replyId, string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
        if (userInfo == null)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        Reply? reply = await _dbContext.Replies.FindAsync((long)replyId);
        if (reply == null)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.REPLY_NOT_EXIST, null);
        }
        reply.Likes -= 1;
        Like? like = await _dbContext.Likes.FirstOrDefaultAsync(like => like.UserId == userId && like.ReplyId == replyId);
        if (like == null)
        {
            return ResponseResult<Reply?>.Fail(ResultCode.LIKE_CANCEL_FAIL, null);
        }
        _dbContext.Likes.Remove(like);
        _dbContext.Replies.Update(reply);
        await _dbContext.SaveChangesAsync();
        return ResponseResult<Reply?>.Success(ResultCode.LIKE_CANCEL_SUCCESS, reply);
    }

    public async Task<ResponseResult<List<Post>>> GetUsersAllPosts(string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
        if (userInfo == null)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.USER_NOT_EXIST, null);
        }

        List<Post> posts = await _dbContext.Posts.Where(post => post.AskerId == userId).ToListAsync();
        if(posts.Count == 0)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.GET_USER_POSTS_FAIL, null);
        }
        return ResponseResult<List<Post>>.Success(ResultCode.GET_USER_POSTS_SUCCESS, posts);
    }

    public async Task<ResponseResult<List<Post>>> GetTenPostsByField(string field, string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
        if (userInfo == null)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        List<Post> posts=await _dbContext.Posts.Where(post=>post.Field == field).ToListAsync();
        if(posts.Count < 10)
        {
            return ResponseResult<List<Post>>.Success(ResultCode.GET_POSTS_SUCCESS, posts);
        }
        List<Post> tenPosts = await _dbContext.Posts.Where(post => post.Field == field).OrderByDescending(post => post.Views).Take(10).ToListAsync();

        return ResponseResult<List<Post>>.Success(ResultCode.GET_TEN_POSTS_SUCCESS, tenPosts);
    }

    public async Task<ResponseResult<List<Post>>> GetAllPosts()
    {
        List<Post> posts = await _dbContext.Posts.ToListAsync();
        if(posts.Count == 0)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.GET_POSTS_FAIL, null);
        }
        return ResponseResult<List<Post>>.Success(ResultCode.GET_POSTS_SUCCESS, posts);
    }

    public async Task<ResponseResult<List<Post>>> GetAllPostsByField(string field, string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)userId);
        if (userInfo == null)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        List<Post> posts = await _dbContext.Posts.Where(post => post.Field == field).ToListAsync();

        if (posts.Count == 0)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.GET_POSTS_FAIL, null);
        }
        return ResponseResult<List<Post>>.Success(ResultCode.GET_POSTS_SUCCESS, posts);
    }

    public async Task<ResponseResult<List<Post>>> GetPostByPostId(int postId)
    {
        List<Post> posts = await _dbContext.Posts.Where(post => post.Id == postId).ToListAsync();
        if (posts.Count() == 0)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.GET_POSTS_BY_ID_FAIL, posts);
        }
        return ResponseResult<List<Post>>.Success(ResultCode.GET_POSTS_BY_ID_SUCCESS, posts);
    }

    public async Task<ResponseResult<List<Post>>> GetPostByTitle(string title)
    {
        List<Post> posts = await _dbContext.Posts.Where(post => post.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        if (posts.Count() == 0)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.GET_POSTS_BY_ID_FAIL, posts);
        }
        return ResponseResult<List<Post>>.Success(ResultCode.GET_POSTS_BY_ID_SUCCESS, posts);
    }
}