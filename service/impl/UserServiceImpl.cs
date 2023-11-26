using Org.BouncyCastle.Asn1.Ocsp;
using YorozuyaServer.common;
using YorozuyaServer.config;
using YorozuyaServer.entity;
using YorozuyaServer.utils;

namespace YorozuyaServer.service.impl;

public class UserServiceImpl : UserService
{
    private readonly DbConfig _dbContext;
    private readonly JwtUtil _jwtUtil;
    private readonly RedisUtil _redisUtil;
    
    public UserServiceImpl(DbConfig dbContext, JwtUtil jwtUtil, RedisUtil redisUtil)
    {
        _dbContext = dbContext;
        _jwtUtil = jwtUtil;
        _redisUtil = redisUtil;
    }
    
    public async Task<ResponseResult<UserInfo?>> Register(string username, string password, string field, int gender)
    {
        UserInfo? user = _dbContext.UserInfos.FirstOrDefault(u => u.Username == username);
        if (user != null)
        {
            return ResponseResult<UserInfo?>.Fail(ResultCode.USER_EXIST, null);
        }

        UserInfo userInfo = new UserInfo
        {
            Username = username,
            Password = password,
            Field = field,
            Gender = gender,
            Role = 0,
            CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
        _dbContext.UserInfos.Add(userInfo);
        await _dbContext.SaveChangesAsync();
        return ResponseResult<UserInfo?>.Success(ResultCode.USER_REGISTER_SUCCESS, null);
    }
    
    public async Task<ResponseResult<Dictionary<string, object>>> Login(string username, string password)
    {
        UserInfo? user = _dbContext.UserInfos.FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            return ResponseResult<Dictionary<string, object>>.Fail(ResultCode.USER_NOT_EXIST, null!);
        }
        if (user.Password != password)
        {
            return ResponseResult<Dictionary<string, object>>.Fail(ResultCode.USER_PASSWORD_ERROR, null!);
        }
        if (_redisUtil.Exists(user.Id.ToString()))
        {
            return ResponseResult<Dictionary<string, object>>.Fail(ResultCode.USER_ALREADY_LOGIN, null!);
        }

        string token = _jwtUtil.GenerateJwtToken(user.Id);
        _redisUtil.Set(user.Id.ToString(), token);
        Dictionary<string, object> data = new()
        {
            {"token", token},
            {"userInfo", user}
        };
        return ResponseResult<Dictionary<string, object>>.Success(ResultCode.USER_LOGIN_SUCCESS, data);
    }
    
    public async Task<ResponseResult<Dictionary<string, object>>> Logout(string token)
    {
        token = token[7..];
        long askerId = long.Parse(_redisUtil.GetKey(token)!);
        if (askerId == 0)
        {
            return ResponseResult<Dictionary<string, object>>.Fail(ResultCode.USER_NOT_EXIST, null!);
        }
        bool tag = _redisUtil.Delete(askerId.ToString());
        if (!tag)
        {
            return ResponseResult<Dictionary<string, object>>.Fail(ResultCode.USER_NOT_EXIST, null!);
        }
        return ResponseResult<Dictionary<string, object>>.Success(ResultCode.SUCCESS, null!);
    }
}