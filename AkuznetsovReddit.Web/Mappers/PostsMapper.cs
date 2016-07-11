using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Web.Models;
using AutoMapper;

namespace AkuznetsovReddit.Web.Mappers
{
    public class PostsMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<PostShortDto, PostShortVm>();
            Mapper.CreateMap<PostFullDto, PostFullVm>();
            Mapper.CreateMap<PostFullVm, PostFullDto>();
        }
    }
}
