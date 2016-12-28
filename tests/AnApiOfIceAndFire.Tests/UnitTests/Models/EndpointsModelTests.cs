using System;
using AnApiOfIceAndFire.Models;
using Xunit;

namespace AnApiOfIceAndFire.Tests.UnitTests.Models
{
    public class EndpointsModelTests
    {
        [Fact]
        public void GivenEmptyBooksUrl_WhenCreatingEndpoints_ThenArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => new EndpointsModel(string.Empty, "http://localhost:55686/api/characters", "http://localhost:55686/api/houses"));
        }

        [Fact]
        public void GivenEmptyCharactersUrl_WhenCreatingEndpoints_ThenArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => new EndpointsModel("http://localhost:55686/api/books", string.Empty, "http://localhost:55686/api/houses"));
        }

        [Fact]
        public void GivenEmptyHousesUrl_WhenCreatingEndpoints_ThenArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => new EndpointsModel("http://localhost:55686/api/books", "http://localhost:55686/api/characters", string.Empty));
        }
    }
}