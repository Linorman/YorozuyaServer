using YorozuyaServer.common;

namespace YorozuyaServer.service;

public interface FileService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="file"></param>
    /// <param name="postId"></param>
    /// <param name="token"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Dictionary<string, object>>> UploadFile(string fileName, IFormFile file, int postId, string token);
    
    /// <summary>
    /// 获取文件url
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="token"></param>
    /// <returns>ResponseResult</returns>
    Task<ResponseResult<Dictionary<string, object>>> GetFileUrl(int postId, string token);
}