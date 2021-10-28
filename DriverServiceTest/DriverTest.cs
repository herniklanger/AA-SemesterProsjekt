using System;
using Xunit;

namespace DriverServiceTest
{
    public class DriverTest
    {
       //test
        [Fact]
        public void GetDriverTest()
        {
            //Arrange
            var driver = new GetCurrentDriver();

            //Act
            var result = driver.IsFound();

            //Assert
            Assert.True(result);
        }
    }
}
