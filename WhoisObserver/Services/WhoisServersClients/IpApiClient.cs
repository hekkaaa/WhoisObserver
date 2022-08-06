using AutoMapper;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.Model.ClientModel;
using WhoisObserver.Services.WhoisServersClients.Interfaces;

namespace WhoisObserver.Services.WhoisServersClients
{
    public class IpApiClient : IWhoisClient
    {
        private string _responseBody = null;
        private IMapper _mapper;

        public IpApiClient(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<string> GetResponceJson(string host)
        {
            string request = $"http://ip-api.com/json/{host}";

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response =(await httpClient.GetAsync(request)).EnsureSuccessStatusCode();
            _responseBody = await response.Content.ReadAsStringAsync();
            
            return _responseBody;
        }

        public async Task<WhoisResponseModel> GetResponceObject(string host)
        {
            _responseBody = await GetResponceJson(host);

            if (_responseBody != null)
            {
                IpApiModel res = JsonSerializer.Deserialize<IpApiModel>(_responseBody);
                WhoisResponseModel result = _mapper.Map<WhoisResponseModel>(res);

                return result;
            }
            else
            {
               return null;
            }
        }
    }
}
