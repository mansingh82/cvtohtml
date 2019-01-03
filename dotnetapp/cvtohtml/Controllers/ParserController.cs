using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cvtohtml.Models;
using System.Text;

namespace cvtohtml.Controllers
{
    [Route("api/[controller]")]
    public class ParserController : Controller
    {
        public readonly IParseService _parseService;

        public ParserController(IParseService parseService) {
            _parseService = parseService;
        }

        [HttpGet("[action]")]
        public IActionResult Parse([FromQuery] string fileLocation) {
            System.Console.Write(fileLocation);
            var result = _parseService.parse(fileLocation);
            return Ok(result);
        }
    }
}