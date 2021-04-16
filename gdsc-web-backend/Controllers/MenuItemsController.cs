using System.Collections.Generic;
using System.Xml.Linq;
using gdsc_web_backend.Models;
using gdsc_web_backend.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class MenuItemsController : ControllerBase
    {
        public List<MenuItemModel> MenuItems = new List<MenuItemModel>
        {
            new MenuItemModel
            {
                Id = "1",
                Name = "site color",
                Type = MenuItemTypeEnum.ExternalLink,
                Link = "www.google.com"
            },
            new MenuItemModel
            {
                Id = "2",
                Name = "language",
                Type = MenuItemTypeEnum.InternalLink,
                Link = "www.linkedin.com"
            }
        };
        
        [HttpGet]
        public List<MenuItemModel> Get()
        {
            return MenuItems;
        }
    
        [HttpGet("{id}")]
        public MenuItemModel Get(string id)
        {
            return MenuItems.Find(x => x.Id == id);
        }
    }
}