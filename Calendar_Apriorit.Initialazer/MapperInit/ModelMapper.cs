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

                
            });
        }
    }
}
