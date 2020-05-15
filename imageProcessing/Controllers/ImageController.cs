using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace imageProcessing.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        readonly ImageProcessing imageProcessing;
        public ImageController(ImageProcessing imageProcessing)
        {
            this.imageProcessing = imageProcessing;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
           return imageProcessing.List();
        }

        // GET api/<controller>/5
        [HttpGet("{uuid}")]
        public ActionResult Get(string uuid)
        {
            var ret = new
            {
                Status = imageProcessing.Status(uuid),
                Return = imageProcessing.Result(uuid),
            };
            return Json(ret);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult<String> Post([FromBody]JsonElement value)
        {
            if (value.TryGetProperty("name", out JsonElement name))
            {
                return imageProcessing.Start(name.ToString());
            }
            return BadRequest("miss 'name' argument");
        }       
    }
}
