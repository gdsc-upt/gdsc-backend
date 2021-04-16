using System.Collections.Generic;
using gdsc_web_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace gdsc_web_backend.Controllers
{
    
    [ApiController] 
    [Route("api/[controller]")]
    public class FaqController: ControllerBase
    {
        
        [HttpGet]
        public List<FaqModel> Get()
        {
            return new List<FaqModel>
            {

                new FaqModel
                {
                    Id="1",
                    Question = "Ce faci ?",
                    Answer = "Uite bine."
                },
                
                new FaqModel
                {
                    Id="2",
                    Question = "Apropo,tu ce faci?",
                    Answer = "Si mai bine decat tine"
                }
            };
        }
    }
}