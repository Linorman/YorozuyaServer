﻿using YorozuyaServer.common;
using YorozuyaServer.entity;

namespace YorozuyaServer.server;

public interface UserService
{
    /// <summary>
    /// 用户注册
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<UserInfo?>> Register(string username, string password, string field, int gender);

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Dictionary<string, string>>> Login(string username, string password);
}