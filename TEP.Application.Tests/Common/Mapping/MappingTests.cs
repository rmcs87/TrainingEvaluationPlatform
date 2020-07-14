using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TEP.Application.Assets.Commands.CreateAsset;
using TEP.Application.Assets.Queries.GetAsset;
using TEP.Application.Common.Mappings;
using TEP.Domain.Entities;

namespace TEP.Application.Tests
{
    [TestClass]
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [TestMethod]
        public void ValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [DataTestMethod]
        [DataRow(typeof(Asset), typeof(AssetDTO))]
        [DataRow(typeof(CreateAssetCommand), typeof(Asset))]
        public void OnMapping_FromSourceToDestination_ShouldSupport(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
