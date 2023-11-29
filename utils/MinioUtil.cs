using Google.Protobuf.WellKnownTypes;
using Minio;
using Minio.DataModel.Args;

namespace YorozuyaServer.utils;

public class MinioUtil
{
    private readonly string bucketName;
    private readonly IMinioClient _minioClient;
    /// <summary>
    /// 初始化方法
    /// </summary>
    public MinioUtil(string api, string accessKey, string secretKey, string bucketName)
    {
        this.bucketName = bucketName;
        _minioClient = new MinioClient()
            .WithEndpoint(api)
            .WithCredentials(accessKey, secretKey)
            .Build();
        if (!BucketExists())
        {
            if (MakeBucket())
                Console.WriteLine("创建存储桶成功");
            else
                Console.WriteLine("创建存储桶失败");
        }
    }

    /// <summary>
    /// 判断存储桶是否存在
    /// </summary>
    /// <param name="bucket"></param>
    /// <returns></returns>
    public bool BucketExists()
    {
        try
        {
            BucketExistsArgs bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(bucketName);
            return _minioClient.BucketExistsAsync(bucketExistsArgs).Result;
        }
        catch (Exception e)
        {
            Console.WriteLine($"判断存储桶是否存在失败,错误信息:{e.Message}");
        }

        return false;
    }

    /// <summary>
    /// 创建存储桶
    /// </summary>
    /// <param name="bucket"></param>
    /// <returns></returns>
    public bool MakeBucket()
    {
        try
        {
            MakeBucketArgs makeBucketArgs = new MakeBucketArgs()
                .WithBucket(bucketName);
            _minioClient.MakeBucketAsync(makeBucketArgs).Wait();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"创建存储桶失败,错误信息:{e.Message}");
        }

        return false;
    }

    /// <summary>
    /// 文件上传
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="inputStream"></param>
    /// <returns></returns>
    public bool Upload(string filePath, Stream inputStream)
    {
        try
        {
            var objectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(filePath)
                .WithStreamData(inputStream);
            _minioClient.PutObjectAsync(objectArgs).Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine($"文件上传失败,错误信息:{e.Message}");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 预览图片
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public string Preview(string fileName)
    {
        try
        {
            var objectArgs = new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithExpiry(60 * 60 * 24 * 21);
            string url = _minioClient.PresignedGetObjectAsync(objectArgs).Result;
            return url;
        }
        catch (Exception e)
        {
            Console.WriteLine($"预览图片失败,错误信息:{e.Message}");
        }

        return null;
    }
}