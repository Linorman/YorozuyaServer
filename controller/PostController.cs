using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YorozuyaServer.config;
using YorozuyaServer.entity;

namespace YorozuyaServer.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly DbConfig _context;

        public PostController(DbConfig context)
        {
            _context = context;
        }

        public class PostRequest
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string Field { get; set; }
        }

        [HttpPost]
        [Authorize]
        public IActionResult PostPost([FromBody] PostRequest request)
        {
            if(request == null)
            {
                return BadRequest("问题格式有误");
            }

            int askerId = 1;//GetUserIdFromToken();

            var post = new Post
            {
                Title = request.Title,
                Content = request.Content,
                AskerId = askerId,
                CreateTime = ,
                UpdateTime = ,
                Views = 0,
                DelTag = 0,
                Field = request.Field
            };

            _context.Posts.Add(post);
            _context.SaveChanges();
        }


    }
}
