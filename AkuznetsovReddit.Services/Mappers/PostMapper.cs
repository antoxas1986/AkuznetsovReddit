using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Entities;
using AutoMapper;

namespace AkuznetsovReddit.Services.Mappers
{
    public class PostMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<PostFullDto, Post>();
        }
    }
}
