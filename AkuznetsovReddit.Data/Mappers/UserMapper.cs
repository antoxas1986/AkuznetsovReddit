using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Entities;
using AutoMapper;

namespace AkuznetsovReddit.Data.Mappers
{
    /// <summary>
    /// Mapper for Users
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Core.Mappers.Interfaces.ICustomMapper" />
    public class UserMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();
            Mapper.CreateMap<User, UserWithUserCredDto>();
            Mapper.CreateMap<UserWithUserCredDto, User>()
                .ForMember(dest => dest.Posts, opts => opts.Ignore());
            Mapper.CreateMap<UserCred, UserCredDto>();
            Mapper.CreateMap<UserCredDto, UserCred>();
        }
    }
}
