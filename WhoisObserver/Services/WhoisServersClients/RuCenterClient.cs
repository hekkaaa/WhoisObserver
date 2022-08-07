using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.Model.InputModel;
using WhoisObserver.Services.Model.OutputModel;
using WhoisObserver.Services.WhoisServersClients.Interfaces;

namespace WhoisObserver.Services.WhoisServersClients
{
    public class RuCenterClient : IWhoisClient
    {
        private const string request = "https://www.nic.ru/app/v1/get/whois";

        public async Task<string> ResponceJson(string host)
        {
            string htmlContent = null;

            using (HttpClient httpClient = new HttpClient())
            {
                RuCenterHeaderOutputModel head = new RuCenterHeaderOutputModel() { lang = "en", searchWord = "213.228.116.142" };
                
                JsonContent jsonHead = JsonContent.Create(head, null);

                HttpResponseMessage response = httpClient.PostAsync(request, jsonHead).Result;
                string responseBody = await response.Content.ReadAsStringAsync();

                RuCenterOriginInputModel account = System.Text.Json.JsonSerializer.Deserialize<RuCenterOriginInputModel>(responseBody);
                htmlContent = account.body.list.First().html;
            }

            if(htmlContent != null)
            {
                char[] delimiterChars = { '\n' };
                string [] words = htmlContent.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);

                List<string> tmpCollection = new List<string>();

                foreach (string word in words)
                {
                    if (!word.Contains('%') && !word.Contains('#'))
                    {
                        tmpCollection.Add(word);
                    }
                }

                return null;
            }
            return null;
        }

        public Task<WhoisResponseModel> ResponceObject(string host)
        {
            throw new NotImplementedException();
        }
    }
}
