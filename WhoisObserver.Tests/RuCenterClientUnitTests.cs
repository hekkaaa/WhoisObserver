using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoisObserver.Services.Mapper;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.WhoisServersClients;

namespace WhoisObserver.Tests
{
    public class RuCenterClientUnitTests
    {
        private IMapper _mapper;
        RuCenterClient _objClient;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ModelMapping>();
            });

            _mapper = config.CreateMapper();
            _objClient = new RuCenterClient(_mapper);
        }

        [Test]
        public async Task OriginalJsonResponceFromServerTest()
        {
            // Arrange
            string hostname = "13.228.116.142";

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
            string hostname = "13.228.116.142";

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
            string hostname = "13.228.116.142";

            // Act
            WhoisResponseModel actual = await _objClient.ResponceObject(hostname);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual.Created);
            Assert.IsNotEmpty(actual.Updated);
            Assert.IsNotEmpty(actual.Org);
            Assert.That(actual.Country, Is.EqualTo("US"));
            Assert.That(actual.RegionName, Is.EqualTo("Seattle"));
            Assert.That(actual.Org, Is.EqualTo("Amazon Technologies Inc."));
            Assert.That(actual.Address, Is.EqualTo("410 Terry Ave N."));
        }
    }
}
