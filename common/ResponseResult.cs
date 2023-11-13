namespace YorozuyaServer.common;

public class ResponseResult<T>
{
    public int Code { get; set; }

    public string Message { get; set; }

    public T Data { get; set; }
    
    private ResponseResult(int code, string message, T data)
    {
        Code = code;
        Message = message;
        Data = data;
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