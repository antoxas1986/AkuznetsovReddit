using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Data.Entities;
using AutoMapper;
using System.Linq;

namespace AkuznetsovReddit.Data.Mappers
{
    /// <summary>
    /// Mapper for Topics
    /// </summary>
    /// <seealso cref="AkuznetsovReddit.Core.Mappers.Interfaces.ICustomMapper" />
    public class TopicMapper : ICustomMapper
    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<Topic, TopicDto>();
            Mapper.CreateMap<TopicDto, Topic>();
            Mapper.CreateMap<Topic, TopicWithPostsDto>()
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts.Where(p => p.IsActive == true)));
            Mapper.CreateMap<Topic, AdminTopicWithPostsDto>();
        }
    }
}
