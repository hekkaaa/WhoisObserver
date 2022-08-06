using AutoMapper;
using System;
using System.Threading.Tasks;
using WhoisObserver.Services;
using WhoisObserver.Services.Mapper;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.WhoisServersClients;

namespace WhoisObserver
{
    public class WhoisClient
    {
        private Context _context;
        private IMapper _mapper;

        public WhoisClient()
        {
            _context = new Context();
            InitializationСConfigureMapper();
        }

        public async Task<string> GetResponceJsonAsync(string host, ServersClientFamily server)
        {
            switch (server)
            {
                case ServersClientFamily.IpApi:
                    _context.SetStrategy(new IpApiClient(_mapper));
                    return await _context.GetResponseJsonAsync(host);

                case ServersClientFamily.RuCenter:
                    //_context.SetStrategy(new IpApiClient(_mapper));
                    break;

                case ServersClientFamily.TestClient:
                    _context.SetStrategy(new TestClient(_mapper));
                    return await _context.GetResponseJsonAsync(host);

                default:
                    throw new ArgumentException("Sorry, specified server is not in the list");
            }

            return null;
        }

        public async Task<WhoisResponseModel> GetResponceModelAsync(string host, ServersClientFamily server)
        {
            switch (server)
            {
                case ServersClientFamily.IpApi:
                    _context.SetStrategy(new IpApiClient(_mapper));
                    return await _context.GetResponseModelAsync(host);

                case ServersClientFamily.RuCenter:
                    //_context.SetStrategy(new IpApiClient(_mapper));
                    break;

                case ServersClientFamily.TestClient:
                    _context.SetStrategy(new TestClient(_mapper));
                    return await _context.GetResponseModelAsync(host);

                default:
                    throw new ArgumentException("Sorry, specified server is not in the list");
            }

            return null;
        }


        private void InitializationСConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ModelMapping>();
            });

            _mapper = config.CreateMapper();
        }
    }
}
