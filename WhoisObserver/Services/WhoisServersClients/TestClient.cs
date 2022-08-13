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

        public Task<WhoisResponseModel> ResponceObject(string host)
        {
            return null;
        }

        public Task<string> ResponceJson(string host)
        {
            return Task.FromResult(string.Empty);
        }

        public Task<string> OriginalJsonResponceFromServer(string host)
        {
            throw new System.NotImplementedException();
        }
    }
}
