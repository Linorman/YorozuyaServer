using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YorozuyaServer.common;
using YorozuyaServer.service;

namespace YorozuyaServer.controller;

[Route("api/file")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly FileService _fileService;
    
    public FileController(FileService fileService)
    {
        _fileService = fileService;
    }
    
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <param name="postId"></param>
    [Authorize]
    [HttpPost("upload")]
    public async Task<ActionResult<ResponseResult<Dictionary<string, object>>>> UploadFile([FromForm] IFormFile file, [FromForm] int postId, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<Dictionary<string, object>> responseResult = await _fileService.UploadFile(file.FileName, file.OpenReadStream(), postId, token);
        return CreatedAtAction(nameof(UploadFile), responseResult);
    }
    
    /// <summary>
    /// 获取文件url
    /// </summary>
    /// <param name="postId"></param>
    [Authorize]
    [HttpGet("url")]
    public async Task<ActionResult<ResponseResult<Dictionary<string, object>>>> GetFileUrl([FromQuery] int postId, [FromHeader(Name = "Authorization")] string token)
    {
        ResponseResult<Dictionary<string, object>> responseResult = await _fileService.GetFileUrl(postId, token);
        return CreatedAtAction(nameof(GetFileUrl), responseResult);
    }
}