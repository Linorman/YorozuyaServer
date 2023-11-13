using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YorozuyaServer.common;
using YorozuyaServer.entity;
using YorozuyaServer.server;


namespace YorozuyaServer.controller
{
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
        public async Task<ActionResult<ResponseResult<UserInfo>>> Register([FromForm] string username, [FromForm] string password, [FromForm] string field, [FromForm] int gender)
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
        public async Task<ActionResult<ResponseResult<Dictionary<string, string>>>> Login([FromForm] string username, [FromForm] string password)
        {
            ResponseResult<Dictionary<string, string>> responseResult = await _userService.Login(username, password);
            return CreatedAtAction(nameof(Login), responseResult);
        }
    }
}
