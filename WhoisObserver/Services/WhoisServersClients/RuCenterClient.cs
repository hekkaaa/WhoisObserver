using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhoisObserver.Services.Helpers;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.Model.InputModel;
using WhoisObserver.Services.Model.OutputModel;
using WhoisObserver.Services.WhoisServersClients.Interfaces;

namespace WhoisObserver.Services.WhoisServersClients
{
    public class RuCenterClient : IWhoisClient
    {
        private const string request = "https://www.nic.ru/app/v1/get/whois";
        private IMapper _mapper;

        private string _htmlContent = null;
        private string _statusRequest = null;
        private List<RuCenterListFormatterInputModel> _formattedContent;

        public RuCenterClient(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<string> OriginalJsonResponceFromServer(string host)
        {
            await CreateRequest(host);

            // hostname type ip-address - '8.8.8.8'
            if (_statusRequest == "ip_info")
            {
                Dictionary<string, string> result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithIpInfo(_htmlContent);

                if (result != null)
                {
                    return RuCenterResponseParserHtml.ConvertInNativeJsonResponce(result);
                }
                return null;
            }

            // hostname type - 'google.com'
            if (_statusRequest == "not_free")
            {
                Dictionary<string, string> result = null;

                if (_formattedContent == null)
                {
                    result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithIpInfo(_htmlContent);
                }
                else
                {
                    result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithNoFree(_formattedContent);
                }

                if (result != null)
                {
                    return RuCenterResponseParserHtml.ConvertInNativeJsonResponce(result);
                }
                return null;
            }

            // not valid hostname\ip-address - '169.254.169.253:2323'
            if (_statusRequest == "fail") return null;

            return null;
        }

        public async Task<string> ResponceJson(string host)
        {

            await CreateRequest(host);

            // hostname type ip-address - '8.8.8.8'
            if (_statusRequest == "ip_info")
            {
                Dictionary<string, string> result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithIpInfo(_htmlContent);

                if (result != null)
                {
                    return RuCenterResponseParserHtml.ConvertDictInJsonIpResponceString(result, _mapper);
                }
                return null;
            }

            // hostname type - 'google.com'
            if (_statusRequest == "not_free")
            {
                Dictionary<string, string> result = null;

                if (_formattedContent == null)
                {
                    result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithIpInfo(_htmlContent);
                }
                else
                {
                    result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithNoFree(_formattedContent);
                }

                if (result != null)
                {
                    return RuCenterResponseParserHtml.ConvertDictInJsonHostnameResponceString(result, _mapper);
                }
                return null;
            }

            // not valid hostname\ip-address - '169.254.169.253:2323'
            if (_statusRequest == "fail") return null;

            return null;
        }


        public async Task<WhoisResponseModel> ResponceObject(string host)
        {
            await CreateRequest(host);

            // hostname type ip-address - '8.8.8.8'
            if (_statusRequest == "ip_info")
            {

                Dictionary<string, string> result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithIpInfo(_htmlContent);

                if (result != null)
                {
                    return RuCenterResponseParserHtml.ConvertDictInWhoisIpModel(result, _mapper);
                }
                return null;
            }

            // hostname type - 'google.com'
            if (_statusRequest == "not_free")
            {
                Dictionary<string, string> result = null;

                if (_formattedContent == null)
                {
                    result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithIpInfo(_htmlContent);
                }
                else
                {
                    result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithNoFree(_formattedContent);
                }

                if (result != null)
                {
                    return RuCenterResponseParserHtml.ConvertDictInWhoisHosnameModel(result, _mapper);
                }
                return null;
            }

            // not valid hostname\ip-address - '169.254.169.253:2323'
            if (_statusRequest == "fail") return null;

            return null;
        }


        private async Task<bool> CreateRequest(string host)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                RuCenterHeaderOutputModel head = new RuCenterHeaderOutputModel() { lang = "en", searchWord = host };

                JsonContent jsonHead = JsonContent.Create(head, null);

                HttpResponseMessage response = httpClient.PostAsync(request, jsonHead).Result;
                string responseBody = await response.Content.ReadAsStringAsync();

                RuCenterOriginInputModel account = System.Text.Json.JsonSerializer.Deserialize<RuCenterOriginInputModel>(responseBody);

                _statusRequest = account.body.status;
                _formattedContent = account.body.list.First().formatted;
                _htmlContent = account.body.list.First().html;
                return true;
            }
        }
    }
}
