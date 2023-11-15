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
    
    // 帖子相关
    public static readonly ResultCode REPLY_PUBLISH_SUCCESS = new(220, "回复发布成功");
    public static readonly ResultCode REPLY_PUBLISH_FAIL = new(420, "回复发布失败");
    public static readonly ResultCode REPLY_DELETE_SUCCESS = new(221, "回复删除成功");
    public static readonly ResultCode REPLY_DELETE_FAIL = new(421, "回复删除失败");
    public static readonly ResultCode REPLY_NOT_EXIST = new(422, "回复不存在");
    public static readonly ResultCode GET_ALL_REPLY_SUCCESS = new(223, "获取全部回复成功");
    public static readonly ResultCode GET_ALL_REPLY_FAIL = new(423, "获取全部回复失败");
    public static readonly ResultCode GET_POST_REPLY_SUCCESS = new(224, "获取帖子回复成功");
    public static readonly ResultCode GET_POST_REPLY_FAIL = new(424, "获取帖子回复失败");
    public static readonly ResultCode ACCEPT_REPLY_SUCCESS = new(225, "采纳回复成功");
    public static readonly ResultCode ACCEPT_REPLY_FAIL = new(425, "采纳回复失败");
    public static readonly ResultCode ACCEPT_REPLY_ALREADY = new(426, "回复已被采纳");
    public static readonly ResultCode ACCEPT_REPLY_NOT_EXIST = new(427, "回复不存在");
    public static readonly ResultCode ACCEPT_REPLY_NOT_OWNER = new(428, "回复不属于该帖子");
    public static readonly ResultCode REPLY_LIKE_SUCCESS = new(226, "回复点赞成功");
    public static readonly ResultCode REPLY_LIKE_FAIL = new(426, "回复点赞失败");
    public static readonly ResultCode REPLY_LIKE_ALREADY_SUCCESS = new(227, "获取点赞状态成功");
#pragma

    public static string GetMessage(ResultCode code)
    {
        return code.Message;
    }
}