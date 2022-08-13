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

            CreateMap<RuCenterIpAddressModel, WhoisResponseModel>()
                .ConvertUsing<PersonConverterJsonRuCenter>();

            CreateMap<RuCenterHostnameModel, WhoisResponseModel>()
                 .ConvertUsing<PersonConverterJsonRuCenterHostModel>();

            CreateMap<WhoisRuModel, WhoisResponseModel>()
                .ConvertUsing<PersonConverterJsonWhoisRu>();
        }
    }

    public class PersonConverterJsonRuCenter : ITypeConverter<RuCenterIpAddressModel, WhoisResponseModel>
    {
        public WhoisResponseModel Convert(RuCenterIpAddressModel source, WhoisResponseModel destination, ResolutionContext context)
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

    public class PersonConverterJsonRuCenterHostModel : ITypeConverter<RuCenterHostnameModel, WhoisResponseModel>
    {
        public WhoisResponseModel Convert(RuCenterHostnameModel source, WhoisResponseModel destination, ResolutionContext context)
        {

            if (source.created is null)
            {
                source.created = source.creationdate;
            }
            if (source.domain is null)
            {
                source.domain = source.domainname;
            }
            if(source.org is null)
            {   
                source.org = source.domain;
                if (source.domain is null) source.org = source.domainname;
            }
            if(source.admincontact is null)
            {
                source.admincontact = source.registrarabusecontactphone;
            }

            destination = new WhoisResponseModel
            {
                Address = null,
                As = null,
                Adminc = null,
                Status = source.state,
                City = null,
                Created = source.created,
                Updated = source.updateddate,
                Inetnum = source.nserver,
                Country = null,
                CountryCode = null,
                Descr = source.nameserver,
                Org = source.org,
                Mntby = null,
                Nettype = null,
                Orgabusehandle = source.registrarianaid,
                Orgabusename = source.registrar,
                Orgabusephone = source.admincontact,
                Region = null,
                Orgid = source.registrarianaid,
                RegionName = null,
                Techc = null,
                Orgtechhandle = source.registrarwhoisserver,
                Isp = source.registrydomainid,
                Zip = null,
                Cidr = null,
                Query = null,
                Timezone = null,
                source = source.registrarwhoisserver,
            };

            return destination;
        }
    }

    public class PersonConverterJsonWhoisRu : ITypeConverter<WhoisRuModel, WhoisResponseModel>
    {
        public WhoisResponseModel Convert(WhoisRuModel source, WhoisResponseModel destination, ResolutionContext context)
        {

            if (source.origin is null)
            {
                source.origin = source.originas;
            }
            if(source.descr is null)
            {
                source.descr = source.comment;
            }
            if (source.inetnum is null)
            {
                source.inetnum = source.netrange;
            }
            if (source.org is null)
            {
                source.org = source.orgname;

                if (source.orgname is null)
                {
                    source.org = source.domain;
                }
            }
            if(source.created is null)
            {
                source.created = source.creation;
            }
            if (source.updated is null)
            {
                source.updated = source.lastmodified;
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
                freedate = source.freedate,
                source = source.source,
                paidtill = source.paidtill,
            };

            return destination;
        }
    }
}

