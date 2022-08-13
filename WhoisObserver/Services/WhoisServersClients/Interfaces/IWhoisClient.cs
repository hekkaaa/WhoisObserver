using System.Threading.Tasks;
using WhoisObserver.Services.Model;

namespace WhoisObserver.Services.WhoisServersClients.Interfaces
{
    public interface IWhoisClient
    {
        Task<string> ResponceJson(string host);
        Task<WhoisResponseModel> ResponceObject(string host);
        Task<string> OriginalJsonResponceFromServer(string host);
    }
}
