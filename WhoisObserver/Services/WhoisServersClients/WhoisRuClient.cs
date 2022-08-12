using AutoMapper;
using System;
using System.Threading.Tasks;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.WhoisServersClients.Interfaces;

namespace WhoisObserver.Services.WhoisServersClients
{
    public class WhoisRuClient : IWhoisClient
    {
        private const string request = "https://whois.ru/";
        private IMapper _mapper;

        public WhoisRuClient(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<string> ResponceJson(string host)
        {
            string url = request + host;
            throw new NotImplementedException();
        }

        public Task<WhoisResponseModel> ResponceObject(string host)
        {
            string url = request + host;
            throw new NotImplementedException();
        }
    }
}
