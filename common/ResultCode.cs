namespace YorozuyaServer.common;

public record ResultCode(int Code, string Message)
{
    #pragma warning disable CA2211
    public static ResultCode SUCCESS = new(200, "响应成功");
    public static ResultCode FAIL = new(400, "响应失败");
    
    // 用户相关
    public static readonly ResultCode USER_REGISTER_SUCCESS = new(201, "用户注册成功");
    public static readonly ResultCode USER_REGISTER_FAIL = new(401, "用户注册失败");
    public static readonly ResultCode USER_LOGIN_SUCCESS = new(202, "用户登录成功");
    public static readonly ResultCode USER_LOGIN_FAIL = new(402, "用户登录失败");
    public static readonly ResultCode USER_NOT_EXIST = new(403, "用户不存在");
    public static readonly ResultCode USER_PASSWORD_ERROR = new(404, "用户密码错误");
    public static readonly ResultCode USER_EXIST = new(405, "用户已存在");
    public static readonly ResultCode USER_ALREADY_LOGIN = new(406, "用户已登录");
    #pragma

    public static string GetMessage(ResultCode code)
    {
        return code.Message;
    }
}