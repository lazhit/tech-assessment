using Accessors.Mapping;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace ManagersTests.MappingTests
{
    [ExcludeFromCodeCoverage]
    public class CustomerMappingProfileTests
    {
        public CustomerMappingProfileTests()
        {
            var mapperConfiguration =
                new MapperConfiguration(config =>
                {
                    config.AddProfile(typeof(CustomerMappingProfile));
                });

            Mapper = mapperConfiguration.CreateMapper();
        }

        private IMapper Mapper { get; set; }

        [Fact]
        public void CustomerMappingProfile_AssertConfigurationIsValid()
        {
            // Assert
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
