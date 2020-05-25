using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TEP.Appication;

namespace TEP.Application.Tests
{
    [TestClass]
    public class MappingEntitiesTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile<MappingEntity>());

            configuration.AssertConfigurationIsValid();
        }
    }
}
