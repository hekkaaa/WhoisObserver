using AutoMapper;
using System;
using System.Threading.Tasks;
using WhoisObserver.Services;
using WhoisObserver.Services.Mapper;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.WhoisServersClients;

namespace WhoisObserver
{

    /// <summary>
    /// Provides the ability to make requests to the WHOIS server from the list.
    /// </summary>
    public class WhoisClient
    {
        private Context _context;
        private IMapper _mapper;

        public WhoisClient()
        {
            _context = new Context();
            InitializationСConfigureMapper();
        }

        /// <summary>
        /// Attempts to create an asynchronous request to the whois server and converts the response to JSON.
        /// </summary>
        /// <param name="host">The address of the remote machine you want to get information about.</param>
        /// <param name="server">The server to which the request will be sent.</param>
        /// <exception cref="ArgumentException">The selected whois server does not exist.</exception>
        public async Task<string> GetResponceJsonAsync(string host, ServersClientFamily server)
        {
            switch (server)
            {
                case ServersClientFamily.IpApi:
                    _context.SetStrategy(new IpApiClient(_mapper));
                    return await _context.GetResponseJsonAsync(host);

                case ServersClientFamily.RuCenter:
                    _context.SetStrategy(new RuCenterClient(_mapper));
                    return await _context.GetResponseJsonAsync(host);

                case ServersClientFamily.WhoisRu:
                    _context.SetStrategy(new WhoisRuClient(_mapper));
                    return await _context.GetResponseJsonAsync(host);

                default:
                    throw new ArgumentException("Sorry, specified server is not in the list");
            }
        }

        /// <summary>
        /// Attempts to create an asynchronous request to the whois server and converts the response to WhoisResponseModel.
        /// </summary>
        /// <param name="host">The address of the remote machine you want to get information about.</param>
        /// <param name="server">The server to which the request will be sent.</param>
        /// <exception cref="ArgumentException">The selected whois server does not exist.</exception>
        public async Task<WhoisResponseModel> GetResponceModelAsync(string host, ServersClientFamily server)
        {
            switch (server)
            {
                case ServersClientFamily.IpApi:
                    _context.SetStrategy(new IpApiClient(_mapper));
                    return await _context.GetResponseModelAsync(host);

                case ServersClientFamily.RuCenter:
                    _context.SetStrategy(new RuCenterClient(_mapper));
                    return await _context.GetResponseModelAsync(host);

                case ServersClientFamily.WhoisRu:
                    _context.SetStrategy(new WhoisRuClient(_mapper));
                    return await _context.GetResponseModelAsync(host);

                default:
                    throw new ArgumentException("Sorry, specified server is not in the list");
            }
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
