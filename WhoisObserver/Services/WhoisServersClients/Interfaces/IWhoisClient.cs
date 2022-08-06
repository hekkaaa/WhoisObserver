using System.Threading.Tasks;
using WhoisObserver.Services.Model;

namespace WhoisObserver.Services.WhoisServersClients.Interfaces
{
    public interface IWhoisClient
    {
        Task<string> GetResponceJson(string host);
        Task<WhoisResponseModel> GetResponceObject(string host);
    }
}
