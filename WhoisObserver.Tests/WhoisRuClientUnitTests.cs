using AutoMapper;
using WhoisObserver.Services.Mapper;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.WhoisServersClients;

namespace WhoisObserver.Tests
{
    public class WhoisRuClientUnitTests
    {
        private IMapper _mapper;
        WhoisRuClient _objClient;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ModelMapping>();
            });

            _mapper = config.CreateMapper();
            _objClient = new WhoisRuClient(_mapper);
        }

        [Test]
        public async Task OriginalJsonResponceFromServerTest()
        {
            // Arrange
            string hostname = "github.com";

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
            string hostname = "github.com";

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
            string hostname = "github.com";

            // Act
            WhoisResponseModel actual = await _objClient.ResponceObject(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual.Created);
            Assert.IsNotEmpty(actual.Updated);
            Assert.IsNotEmpty(actual.Org);
        }
    }
}