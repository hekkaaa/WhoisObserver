using AutoMapper;
using WhoisObserver.Services.Mapper;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.WhoisServersClients;

namespace WhoisObserver.Tests
{
    public class IpApiClientUnitTests
    {
        private IMapper _mapper;
        IpApiClient _objClient;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ModelMapping>();
            });

            _mapper = config.CreateMapper();
            _objClient = new IpApiClient(_mapper);
        }

        [Test]
        public async Task OriginalJsonResponceFromServerTest()
        {
            // Arrange
            string hostname = "8.8.8.8";

            // Act
            var actual = await _objClient.OriginalJsonResponceFromServer(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual);
        }

        [Test]
        public async Task ResponceJsonTest()
        {
            // Arrange
            string hostname = "8.8.8.8";

            // Act
            var actual = await _objClient.ResponceJson(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual);
        }

        [Test]
        public async Task ResponceObjectTest()
        {
            // Arrange
            string hostname = "8.8.8.8";

            // Act
            WhoisResponseModel actual = await _objClient.ResponceObject(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual.Created);
            Assert.IsNotEmpty(actual.Updated);
            Assert.That(actual.As, Is.EqualTo("AS15169 Google LLC"));
            Assert.That(actual.RegionName, Is.EqualTo("Virginia"));
            Assert.That(actual.Isp, Is.EqualTo("Google LLC"));
            Assert.That(actual.Query, Is.EqualTo("8.8.8.8"));
        }
    }
}
