using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Web.Models;
using AutoMapper;

namespace AkuznetsovReddit.Web.Mappers
{
    public class RegisterMapper : ICustomMapper

    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<RegisterVm, RegisterDto>();
        }
    }
}
