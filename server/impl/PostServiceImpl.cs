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

    public async Task<ResponseResult<Post>> PublishPost(string title, string content, string field, string token)
    {
        token = token[7..];
        Int32 askerId = Int32.Parse(_redisUtil.GetKey(token)!);

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(askerId);
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

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(askerId);
        if (userInfo == null)
        {
            return ResponseResult<Post?>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        Post? post = await _dbContext.Posts.FindAsync(postId);

        if (post == null)
        {
            return ResponseResult<Post?>.Fail(ResultCode.POST_NOT_EXIST, null);
        }
        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync();
        return ResponseResult<Post?>.Success(ResultCode.POST_DELETE_SUCCESS, post);
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

    public async Task<ResponseResult<Reply?>> CancelLike(int replyId, string token)
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
        reply.Likes -= 1;
        Like like = await _dbContext.Likes.FindAsync(replyId, userId);
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

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(userId);
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

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(userId);
        if (userInfo == null)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.USER_NOT_EXIST, null);
        }
        List<Post> posts=await _dbContext.Posts.Where(post=>post.Field == field).ToListAsync();
        if(posts.Count < 10)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.GET_TEN_POSTS_FAIL, null);
        }
        //随机给出十个
        //// 使用 Fisher-Yates 算法进行洗牌
        //Random random = new Random();
        //int n = posts.Count;
        //for (int i = n - 1; i > 0; i--)
        //{
        //    int j = random.Next(0, i + 1);
        //    // 交换元素
        //    Post temp = posts[i];
        //    posts[i] = posts[j];
        //    posts[j] = temp;
        //}

        //// 选择前十个元素
        //List<Post> tenposts = posts.Take(10).ToList();

        //按照浏览量给出十个
        List<Post> tenposts = await _dbContext.Posts.Where(post => post.Field == field).OrderByDescending(post => post.Views).Take(10).ToListAsync();

        return ResponseResult<List<Post>>.Success(ResultCode.GET_TEN_POSTS_SUCCESS, tenposts);
    }

    public async Task<ResponseResult<List<Post>>> GetAllPosts(string token)
    {
        token = token[7..];
        Int32 userId = Int32.Parse(_redisUtil.GetKey(token)!);

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(userId);
        if (userInfo == null)
        {
            return ResponseResult<List<Post>>.Fail(ResultCode.USER_NOT_EXIST, null);
        }

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

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync(userId);
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
}