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

        public RuCenterClient(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<string> ResponceJson(string host)
        {
            string htmlContent = null;
            string statusRequest = null;

            using (HttpClient httpClient = new HttpClient())
            {
                RuCenterHeaderOutputModel head = new RuCenterHeaderOutputModel() { lang = "en", searchWord = host };
                
                JsonContent jsonHead = JsonContent.Create(head, null);

                HttpResponseMessage response = httpClient.PostAsync(request, jsonHead).Result;
                string responseBody = await response.Content.ReadAsStringAsync();

                RuCenterOriginInputModel account = System.Text.Json.JsonSerializer.Deserialize<RuCenterOriginInputModel>(responseBody);

                statusRequest = account.body.status;
                htmlContent = account.body.list.First().html;
            }

            // hostname type ip-address - '8.8.8.8'
            if(statusRequest == "ip_info")
            {

                Dictionary<string, string> result = RuCenterResponseParserHtml.ParseHtmlResponseContentWithIpInfo(htmlContent);
                //if (htmlContent != null)
                //{
                //    char[] delimiterChars = { '\n', '\r' };
                //    string[] splitHtmlContent = htmlContent.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);

                //    //List<string> tmpCollection = new List<string>();
                //    Dictionary<string, string> tmpDict = new Dictionary<string, string>();

                //    foreach (string valuesContents in splitHtmlContent)
                //    {
                //        if (!valuesContents.Contains('%') && !valuesContents.Contains('#'))
                //        {
                //            try
                //            {
                //                // Split string key and value.
                //                string[] tmpValuesContents = valuesContents.Split(':');

                //                // clearing the key of unnecessary characters '-', ' '.
                //                string[] tmpClearedKeyArrayFromChar = tmpValuesContents[0].Split('-', ' ');
                //                tmpValuesContents[0] = string.Empty;

                //                // rebuild key after Split.
                //                foreach (var key in tmpClearedKeyArrayFromChar)
                //                {
                //                    tmpValuesContents[0] = string.Concat(tmpValuesContents[0], key.ToLower());
                //                }

                //                tmpDict.Add(tmpValuesContents[0], tmpValuesContents[1].Trim(new char[] { ' ' }));
                //            }
                //            catch (System.ArgumentException)
                //            {
                //                // Error when repeating the key.
                //                continue;
                //            }
                //        }


                //    }

                //    return null;
                //}

                if(result != null)
                {
                    var end = RuCenterResponseParserHtml.ConvertInJsonWithNativeResponce(result);
                    return end;
                }
                return null;
            }

            // hostname type - 'google.com'
            if(statusRequest == "not_free")
            {
                return null;
            }
            // not valid hostname\ip-address - '169.254.169.253:2323'
            if (statusRequest == "fail")
            {

            }

            
            return null;
        }

        public Task<WhoisResponseModel> ResponceObject(string host)
        {
            throw new NotImplementedException();
        }
    }
}
