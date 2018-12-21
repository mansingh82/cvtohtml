using System;
using cvtohtml.Controllers;
using Xunit;

namespace cvtohtml.tests
{
    public class ParserControllerTest
    {

        ParserController _controller;

        public ParserControllerTest() {
            _controller = new ParserController();
        }

        [Fact]
        public void ParseContent()
        {
            Assert.True(_controller.Parse());
        }
    }
}
