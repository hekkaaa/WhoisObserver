using AutoMapper;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using WhoisObserver.Services.Helpers;
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

        public async Task<string> OriginalJsonResponceFromServer(string host)
        {
            string url = request + host;
            string resultResponce = HtmlAgilityPack(url);
            Dictionary<string, string> result = WhoisRuResponceParserHtml.ParseHtmlResponseContent(resultResponce);

            if (result != null)
            {
                return WhoisRuResponceParserHtml.ConvertInNativeJsonResponce(result);
            }

            return null;
        }

        public async Task<string> ResponceJson(string host)
        {
            string url = request + host;
            string resultResponce = HtmlAgilityPack(url);
            Dictionary<string, string> result = WhoisRuResponceParserHtml.ParseHtmlResponseContent(resultResponce);

            if (result != null)
            {
                return WhoisRuResponceParserHtml.ConvertDictInJsonIpResponceString(result, _mapper);
            }

            return null;
        }

        public async Task<WhoisResponseModel> ResponceObject(string host)
        {
            string url = request + host;
            string resultResponce = HtmlAgilityPack(url);
            Dictionary<string, string> result = WhoisRuResponceParserHtml.ParseHtmlResponseContent(resultResponce);

            if (result != null)
            {
                return WhoisRuResponceParserHtml.ConvertDictInWhoisRuModel(result, _mapper);
            }

            return null;
        }


        private string HtmlAgilityPack(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            HtmlNode collection = doc.GetElementbyId("id");
            HtmlNodeCollection span = doc.DocumentNode.SelectNodes("//pre[contains(@class, 'raw-domain-info-pre')]");

            return span.First().InnerText;
        }
    }
}
