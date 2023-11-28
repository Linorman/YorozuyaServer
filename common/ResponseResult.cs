namespace YorozuyaServer.common;

public class ResponseResult<T>
{
    public int code { get; set; }

    public string msg { get; set; }

    public T data { get; set; }
    
    private ResponseResult(int code, string message, T data)
    {
        this.code = code;
        this.msg = message;
        this.data = data;
    }

    public static ResponseResult<T> Success(T data)
    {
        return new ResponseResult<T>(200, "success", data);
    }
    
    public static ResponseResult<T> Success(ResultCode resultCode, T data)
    {
        return new ResponseResult<T>(resultCode.Code, resultCode.Message, data);
    }

    public static ResponseResult<T> Fail(ResultCode resultCode, T data)
    {
        return new ResponseResult<T>(resultCode.Code, resultCode.Message, data);
    }
}

