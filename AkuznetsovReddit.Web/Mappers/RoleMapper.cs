using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Web.Models;
using AutoMapper;
using System.Web.Mvc;

namespace AkuznetsovReddit.Web.Mappers
{
    public class RoleMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<RoleDto, RoleVm>();

            Mapper.CreateMap<RoleDto, SelectListItem>()
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.RoleId.ToString()))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => src.RoleName));
        }
    }
}
