using System;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [ApiVersion("1")]
    [Route("api/books")]
    public class BooksController : Controller
    {
        public const string SingleBookRouteName = "SingleBookEndpoint";
        public const string MultipleBooksRouteName = "MultipleBooksEndpoint";

        [HttpGet]
        [Route("{id:int}", Name = SingleBookRouteName)]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [HttpHead]
        [Route("", Name = MultipleBooksRouteName)]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }
    }
}