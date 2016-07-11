using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkuznetsovReddit.Services.Mappers
{
    public class RegisterMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            //Mapper.CreateMap<RegisterDto, UserDto>()
            //    .ForMember(dest=>dest.UserName, opts=>opts.MapFrom(src=>src.UserName))
            //    .ForMember(dest=>dest.Pa);
        }
    }
}
