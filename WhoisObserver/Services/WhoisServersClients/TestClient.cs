using AutoMapper;
using System.Threading.Tasks;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.WhoisServersClients.Interfaces;

namespace WhoisObserver.Services.WhoisServersClients
{
    public class TestClient : IWhoisClient
    {
        private IMapper _mapper;

        public TestClient(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<WhoisResponseModel> GetResponceObject(string host)
        {
            return null;
        }

        public Task<string> GetResponceJson(string host)
        {
            return Task.FromResult(string.Empty);
        }
    }
}
