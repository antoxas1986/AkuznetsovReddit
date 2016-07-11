using AkuznetsovReddit.Core.Mappers.Interfaces;
using AkuznetsovReddit.Core.Models;
using AkuznetsovReddit.Web.Models;
using AutoMapper;

namespace AkuznetsovReddit.Web.Mappers
{
    public class TopicMapper : ICustomMapper

    {
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<TopicDto, TopicVm>();
            Mapper.CreateMap<TopicVm, TopicDto>();
            Mapper.CreateMap<TopicWithPostsDto, TopicWithPostsVm>();
            Mapper.CreateMap<AdminTopicWithPostsDto, TopicWithPostsVm>();
        }
    }
}
