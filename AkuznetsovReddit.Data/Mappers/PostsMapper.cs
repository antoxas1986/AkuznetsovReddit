using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Entities;
using AutoMapper;

namespace AkuznetsovReddit.Data.Mappers
{
    /// <summary>
    /// Mapper for Posts
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Core.Mappers.Interfaces.ICustomMapper" />
    public class PostsMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<Post, PostShortDto>();
            Mapper.CreateMap<Post, AdminPostShortDto>();
            Mapper.CreateMap<Post, PostFullDto>();
            Mapper.CreateMap<PostFullDto, Post>()
             .ForMember(dest => dest.User, opts => opts.Ignore())
             .ForMember(dest => dest.Topic, opts => opts.Ignore());
        }
    }
}
