﻿using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Web.Models;
using AutoMapper;

namespace AkuznetsovReddit.Web.Mappers
{
    public class UserMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<UserDto, UserVm>();
        }
    }
}
