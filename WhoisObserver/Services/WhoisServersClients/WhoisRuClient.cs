using AutoMapper;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
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
            string HtmlAgilityPack()
            {
                //var url = "https://whois.ru/8.8.8.8";
                //var url = "https://whois.ru/185.140.148.159";
                //var url = "https://whois.ru/ya.ru";
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);

                HtmlNode collection = doc.GetElementbyId("id");
                HtmlNodeCollection span = doc.DocumentNode.SelectNodes("//pre[contains(@class, 'raw-domain-info-pre')]");

                return span.First().InnerText;
            }

        }

        public Task<WhoisResponseModel> ResponceObject(string host)
        {
            string url = request + host;
            throw new NotImplementedException();
        }
    }
}
