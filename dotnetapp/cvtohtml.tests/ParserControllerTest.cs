using System;
using Microsoft.AspNetCore.Mvc;
using cvtohtml.Controllers;
using Xunit;
using cvtohtml.Models;

namespace cvtohtml.tests
{
    public class ParserControllerTest
    {
        IParseService _service;
        ParserController _controller;

        public ParserControllerTest() {
            _service = new ParseService();
            _controller = new ParserController(_service);
        }

        [Fact]
        public void ParseContent()
        {
            var actionResult = _controller.Parse("./test_resources/demo.docx");

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value;
            Assert.NotNull(model);

            Assert.True((bool)model);
        }
    }
}
