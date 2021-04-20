using System.Collections.Generic;
using System.Net;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagesController : ControllerBase
    {
        public List<PageModel> pageModels = new List<PageModel>
        {
            new PageModel()
            {
                Id = "1",
                Title = "Home",
                Body = "In progress",
                isPublished = false,
                Slug = "home-page",
                ShortDescription = "Shows a description about the site",
                Image = "Smth 'bout Google",
            },
            new PageModel()
            {
                Id = "2",
                Title = "Contact",
                Body = "Still in progress",
                isPublished = false,
                Slug = "contact",
                ShortDescription = "Some data about us and how you could get in touch",
                Image = "Smth 'bout us",
            }
        };

        [HttpGet("{slug}")]
        public PageModel Get(string slug)
        {
            return pageModels.Find(page => page.Slug == slug);
        }
    }
}