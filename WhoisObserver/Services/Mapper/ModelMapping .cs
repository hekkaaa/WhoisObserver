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
            CreateMap<RuCenterModel, WhoisResponseModel>()
                .ConvertUsing<PersonConverterJsonRuCenter>();
        }
    }

    public class PersonConverterJsonRuCenter : ITypeConverter<RuCenterModel, WhoisResponseModel>
    {
        public WhoisResponseModel Convert(RuCenterModel source, WhoisResponseModel destination, ResolutionContext context)
        {

           if(source.created is null)
            {
                source.created = source.regdate;
            }
           if(source.inetnum is null)
            {
                source.inetnum = source.netrange;
            }
           if(source.updated is null)
            {
                source.updated = source.lastmodified;
            }
            if(source.org is null)
            {
                source.org = source.orgname;
            }
            if(source.descr is null)
            {
                source.descr = source.comment;
            }

            destination = new WhoisResponseModel
            {
                Address = source.address,
                As = source.origin,
                Adminc = source.adminc,
                Status = source.status,
                City = source.city,
                Created = source.created,
                Updated = source.updated,
                Inetnum = source.inetnum,
                Country = source.country,
                CountryCode = source.country,
                Descr = source.descr,
                Org = source.org,
                Mntby = source.mntby,
                Nettype = source.nettype,
                Orgabusehandle = source.orgabusehandle,
                Orgabusename = source.orgabusename,
                Orgabusephone = source.orgabusephone,
                Region = source.country,
                Orgid = source.orgid,
                RegionName = source.city,
                Techc = source.techc,
                Orgtechhandle = source.orgtechhandle,
                Isp = source.netname,
                Zip = source.postalcode,
                Cidr = source.cidr,
                Query = null,
                Timezone = null,
            };

            return destination;
        }
    }

}

