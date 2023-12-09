using YorozuyaServer.common;
using YorozuyaServer.config;
using YorozuyaServer.entity;
using YorozuyaServer.utils;

namespace YorozuyaServer.service.impl;

public class FileServiceImpl : FileService
{
    private readonly MinioUtil _minioUtil;
    private readonly RedisUtil _redisUtil;
    private readonly DbConfig _dbContext;
    
    public FileServiceImpl(MinioUtil minioUtil, RedisUtil redisUtil, DbConfig dbContext)
    {
        _minioUtil = minioUtil;
        _redisUtil = redisUtil;
        _dbContext = dbContext;
    }
    
    public async Task<ResponseResult<Dictionary<string, object>>> UploadFile(string fileName, IFormFile file, int postId, string token)
    {
        token = token[7..];
        Int32 askerId = Int32.Parse(_redisUtil.GetKey(token)!);

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)askerId);
        if (userInfo == null)
        {
            return ResponseResult<Dictionary<string, object>>.Fail(ResultCode.USER_NOT_EXIST, null);
        }

        String objName = "post-" + "postId-" + postId + "-" + fileName;
        bool flag = await _minioUtil.Upload(objName, file);
        if (flag)
        {
            string url = "minio:9000" + "/yorozuya/" + objName;
            Image image = new()
            {
                ImageUrl = url,
                PostId = postId
            };
            _dbContext.Images.Add(image);
            await _dbContext.SaveChangesAsync();
            Dictionary<string, object> dictionary = new()
            {
                {"imageUrl", url}
            };
            return ResponseResult<Dictionary<string, object>>.Success(ResultCode.FILE_UPLOAD_SUCCESS, dictionary);
        }
        else
        {
            return ResponseResult<Dictionary<string, object>>.Fail(ResultCode.FILE_UPLOAD_FAIL, null!);
        }
    }
    
    public async Task<ResponseResult<Dictionary<string, object>>> GetFileUrl(int postId, string token)
    {
        token = token[7..];
        Int32 askerId = Int32.Parse(_redisUtil.GetKey(token)!);

        UserInfo? userInfo = await _dbContext.UserInfos.FindAsync((long)askerId);
        if (userInfo == null)
        {
            return ResponseResult<Dictionary<string, object>>.Fail(ResultCode.USER_NOT_EXIST, null);
        }

        Image image = await _dbContext.Images.FindAsync((long)postId);
        Dictionary<string, object> dictionary = new()
        {
            {"imageUrl", image.ImageUrl}
        };
        return ResponseResult<Dictionary<string, object>>.Success(ResultCode.FILE_GET_SUCCESS, dictionary);
    }
}