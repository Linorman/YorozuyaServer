using YorozuyaServer.common;
using YorozuyaServer.config;
using YorozuyaServer.entity;
using YorozuyaServer.utils;

namespace YorozuyaServer.server.impl
{
    public class PostServiceImpl
    {
        private readonly DbConfig _dbContext;
        private readonly JwtUtil _jwtUtil;
        private readonly RedisUtil _redisUtil;

        public PostServiceImpl(DbConfig dbContext, JwtUtil jwtUtil, RedisUtil redisUtil)
        {
            _dbContext = dbContext;
            _jwtUtil = jwtUtil;
            _redisUtil = redisUtil;
        }

        public async ResponseResult<Post> Publish(string title, string content, string field)
        {
            try
            {
                // Validate input parameters (you can add more validation logic as needed)
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content) || string.IsNullOrEmpty(field))
                {
                    return ResponseResult<Post>.Error("Title, content, and field are required.");
                }

                // Create a new Post instance
                var newPost = new Post
                {
                    Title = title,
                    Content = content,
                    Field = field,
                    // You might set other properties here
                };

                // Perform any additional business logic or validation before saving to the database

                // Save the post to the database
                _dbContext.Posts.Add(newPost);
                await _dbContext.SaveChangesAsync();

                // You can add additional logic here, such as caching or generating a JWT token

                return ResponseResult<Post>.Success(newPost);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return ResponseResult<Post>.Error($"An error occurred: {ex.Message}");
            }
        }

    }
}
