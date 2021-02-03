using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace expertDotNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BloggingContext bloggingContext;
        public BlogController()
        {
            bloggingContext = new BloggingContext();
        }
        [HttpGet]
        public ActionResult<List<Blog>> Get()
        {
            return bloggingContext.Blogs.ToList<Blog>();
        }

        [HttpPost]
        public ActionResult<Blog> Create(Blog blog)
        {
            try
            {
                bloggingContext.Add(blog);
                bloggingContext.SaveChanges();
                return Created("Blog", blog);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPut]
        public ActionResult<Blog> Put(Blog blog)
        {
            try
            {
                bloggingContext.Update(blog);
                bloggingContext.SaveChanges();
                return Created("Blog", blog);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var blog = bloggingContext.Blogs.Where(blog => blog.BlogId == id).FirstOrDefault();

            if (blog == null)
                return NotFound();

            bloggingContext.Blogs.Remove(blog);
            bloggingContext.SaveChanges();
            return NoContent();
        }
    }
}
