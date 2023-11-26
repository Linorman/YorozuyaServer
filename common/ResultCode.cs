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
    public static readonly ResultCode POST_PUBLISH_SUCCESS = new(203, "发布问题成功");
    public static readonly ResultCode POST_PUBLISH_FAIL = new(407, "发布问题失败");
    public static readonly ResultCode POST_DELETE_SUCCESS = new(204, "删除问题成功");
    public static readonly ResultCode POST_DELETE_FAIL = new(408, "删除问题失败");
    public static readonly ResultCode POST_NOT_EXIST = new(409, "问题不存在");
    public static readonly ResultCode LIKE_CANCEL_FAIL = new(410, "取消点赞失败");
    public static readonly ResultCode LIKE_CANCEL_SUCCESS = new(205, "取消点赞成功");
    public static readonly ResultCode GET_USER_POSTS_FAIL = new(411, "获取用户问题失败");
    public static readonly ResultCode GET_USER_POSTS_SUCCESS = new(206, "获取用户问题成功");
    public static readonly ResultCode GET_TEN_POSTS_FAIL = new(412, "获取相应领域问题失败");
    public static readonly ResultCode GET_TEN_POSTS_SUCCESS = new(207, "获取相应领域问题成功");
    public static readonly ResultCode GET_POSTS_SUCCESS = new(208, "获取问题成功");
    public static readonly ResultCode GET_POSTS_FAIL = new(413, "获取问题失败");
    public static readonly ResultCode GET_POSTS_BY_ID_SUCCESS = new(208, "查找问题成功");
    public static readonly ResultCode GET_POSTS_BY_ID_FAIL = new(414, "查找问题失败");



    public static readonly ResultCode REPLY_PUBLISH_SUCCESS = new(220, "回复发布成功");
    public static readonly ResultCode REPLY_PUBLISH_FAIL = new(420, "回复发布失败");
    public static readonly ResultCode REPLY_DELETE_SUCCESS = new(221, "回复删除成功");
    public static readonly ResultCode REPLY_DELETE_FAIL = new(421, "回复删除失败");
    public static readonly ResultCode REPLY_NOT_EXIST = new(422, "回复不存在");
    public static readonly ResultCode GET_ALL_REPLY_SUCCESS = new(223, "获取全部回复成功");
    public static readonly ResultCode GET_ALL_REPLY_FAIL = new(423, "获取全部回复失败");
    public static readonly ResultCode GET_POST_REPLY_SUCCESS = new(224, "获取问题回复成功");
    public static readonly ResultCode GET_POST_REPLY_FAIL = new(424, "获取问题回复失败");
    public static readonly ResultCode ACCEPT_REPLY_SUCCESS = new(225, "采纳回复成功");
    public static readonly ResultCode ACCEPT_REPLY_FAIL = new(425, "采纳回复失败");
    public static readonly ResultCode ACCEPT_REPLY_ALREADY = new(426, "回复已被采纳");
    public static readonly ResultCode ACCEPT_REPLY_NOT_EXIST = new(427, "回复不存在");
    public static readonly ResultCode ACCEPT_REPLY_NOT_OWNER = new(428, "回复不属于该问题");
    public static readonly ResultCode REPLY_LIKE_SUCCESS = new(226, "回复点赞成功");
    public static readonly ResultCode REPLY_LIKE_FAIL = new(426, "回复点赞失败");
    public static readonly ResultCode REPLY_LIKE_ALREADY_SUCCESS = new(227, "获取点赞状态成功");
    #pragma

    public static string GetMessage(ResultCode code)
    {
        return code.Message;
    }
}