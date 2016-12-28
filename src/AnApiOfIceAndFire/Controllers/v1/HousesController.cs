using System;
using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Controllers.v1
{
    [Route("api/houses")]
    public class HousesController : Controller
    {
        public const string SingleHouseRouteName = "SingleHouseEndpoint";
        public const string MultipleHousesRouteName = "MultipleHousesEndpoint";

        [HttpGet]
        [Route("{id:int}", Name = SingleHouseRouteName)]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [HttpHead]
        [Route("", Name = MultipleHousesRouteName)]
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }
    }
}