using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AnApiOfIceAndFire.Tests
{
    public class Class1
    {
        [Fact]
        public void ThisTestShouldPass()
        {
            //We use this dummy test to test if the CI-environment runs the tests properly. Remove this after verifying that stuff works
            Assert.Equal(2, (1+1));
        }
    }
}
