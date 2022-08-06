using AutoMapper;
using WhoisObserver.Services.Model;
using WhoisObserver.Services.Model.ClientModel;

namespace WhoisObserver.Services.Mapper
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<WhoisResponseModel, IpApiModel>().ReverseMap();
            //.ForMember(w => w.City, x => x.MapFrom(ip => ip.region));
        }
    }
}
