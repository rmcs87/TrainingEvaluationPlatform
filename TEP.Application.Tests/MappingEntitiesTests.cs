using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TEP.Application;

namespace TEP.Application.Tests
{
    [TestClass]
    public class MappingEntitiesTests
    {
        [TestMethod]
        public void CanConfigureApplicationMapper()
        {
            //Arrange
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<MappingEntity>());
            //Act

            //Assert
            configuration.AssertConfigurationIsValid();
        }
    }
}
