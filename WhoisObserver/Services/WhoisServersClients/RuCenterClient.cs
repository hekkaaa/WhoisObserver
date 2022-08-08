using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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

        private string htmlContent = null;
        private string statusRequest = null;

        public RuCenterClient(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<string> ResponceJson(string host)
        {

            await CreateRequest(host);

            // hostname type ip-address - '8.8.8.8'
            if (statusRequest == "ip_info")
            {

                Dictionary<string, string> result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithIpInfo(htmlContent);

                if (result != null)
                {
                    return RuCenterResponseParserHtml.ConvertDictInJsonResponceString(result, _mapper);
                }
                return null;
            }

            // hostname type - 'google.com'
            if (statusRequest == "not_free")
            {
                //Dictionary<string, string> result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithIpInfo(htmlContent);

                //if (result != null)
                //{
                //    return RuCenterResponseParserHtml.ConvertDictInJsonResponceString(result, _mapper);
                //}
                //return null;
            }

            // not valid hostname\ip-address - '169.254.169.253:2323'
            if (statusRequest == "fail") return null;

            return null;
        }


        public async Task<WhoisResponseModel> ResponceObject(string host)
        {
            await CreateRequest(host);

            // hostname type ip-address - '8.8.8.8'
            if (statusRequest == "ip_info")
            {

                Dictionary<string, string> result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithIpInfo(htmlContent);

                if (result != null)
                {
                    return RuCenterResponseParserHtml.ConvertDictInWhoisModel(result, _mapper);
                }
                return null;
            }

            // hostname type - 'google.com'
            if (statusRequest == "not_free")
            {
               // TODO
            }

            // not valid hostname\ip-address - '169.254.169.253:2323'
            if (statusRequest == "fail") return null;

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

                statusRequest = account.body.status;
                htmlContent = account.body.list.First().html;
                return true;
            }
        }
    }
}
