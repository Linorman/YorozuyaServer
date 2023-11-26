using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YorozuyaServer.common;
using YorozuyaServer.entity;
using YorozuyaServer.service;


[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 用户注册
    /// </summary>
    /// <param name="username, password"></param>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<ResponseResult<UserInfo>>> Register([FromForm] string username,
        [FromForm] string password, [FromForm] string field, [FromForm] int gender)
    {
        ResponseResult<UserInfo?> responseResult = await _userService.Register(username, password, field, gender);
        return CreatedAtAction(nameof(Register), responseResult);
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="username, password"></param>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<ResponseResult<Dictionary<string, object>>>> Login([FromForm] string username,
        [FromForm] string password)
    {
        ResponseResult<Dictionary<string, object>> responseResult = await _userService.Login(username, password);
        return CreatedAtAction(nameof(Login), responseResult);
    }
    
    /// <summary>
    /// 用户登出
    /// </summary>
    /// <param></param>
    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult<ResponseResult<Dictionary<string, string>>>> Logout([FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<Dictionary<string, object>> responseResult = await _userService.Logout(token);
        return CreatedAtAction(nameof(Logout), responseResult);
    }
}