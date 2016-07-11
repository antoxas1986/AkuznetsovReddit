using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Entities;
using AutoMapper;

namespace AkuznetsovReddit.Data.Mappers
{
    /// <summary>
    /// Mapper for Roles
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Core.Mappers.Interfaces.ICustomMapper" />
    public class RoleMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<Role, RoleDto>();
            Mapper.CreateMap<RoleDto, Role>();
        }
    }
}
