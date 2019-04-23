using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar_Apriorit.DAL.Entities;
using Calendar_Apriorit.ViewModel;

namespace Calendar_Apriorit.Initialazer.MapperInit
{
    public class ModelMapper
    {
        public static void Init()
        {
            AutoMapper.Mapper.Initialize((map) =>
            {
                //map.CreateMap<User, RegisterVM>().ForMember(dest => dest.Email,opts => opts.MapFrom(src => src.Email))
                map.CreateMap<EventInfoVM, EventInfo>().ReverseMap();
                map.CreateMap<RepeatInfoVM, RepeatInfo>().ReverseMap();
                map.CreateMap<EventVM, Event>().ForMember(dest => dest.Id, dest => dest.MapFrom(src => src.IdEvent));
                map.CreateMap<Event, EventVM>().ForMember(dest => dest.IdEvent, dest => dest.MapFrom(src => src.Id));


            });
        }
    }
}
