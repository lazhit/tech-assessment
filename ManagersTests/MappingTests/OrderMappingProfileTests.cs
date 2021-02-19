using AutoMapper;
using Managers.Mapping;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace ManagersTests.MappingTests
{
    [ExcludeFromCodeCoverage]
    public class OrderMappingProfileTests
    {
        public OrderMappingProfileTests()
        {
            var mapperConfiguration =
                new MapperConfiguration(config =>
                {
                    config.AddProfile(typeof(OrderMappingProfile));
                });

            Mapper = mapperConfiguration.CreateMapper();
        }

        private IMapper Mapper { get; set; }

        [Fact]
        public void OrderMappingProfile_AssertConfigurationIsValid()
        {
            // Assert
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
