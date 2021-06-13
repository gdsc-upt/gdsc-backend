using AutoMapper;
using GdscBackend.Models;

namespace GdscBackend.RequestModels
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap(typeof(TechnologyModel), typeof(TechnologyRequest)).ReverseMap();
            CreateMap(typeof(FaqModel), typeof(FaqRequest)).ReverseMap();
            CreateMap(typeof(EventModel), typeof(EventRequest)).ReverseMap();
            CreateMap(typeof(MemberModel), typeof(MemberRequest)).ReverseMap();
            CreateMap(typeof(MenuItemModel), typeof(MenuItemRequest)).ReverseMap();
            CreateMap(typeof(PageModel), typeof(PageRequest)).ReverseMap();
            CreateMap(typeof(SettingModel), typeof(SettingRequest)).ReverseMap();
            CreateMap(typeof(TeamModel), typeof(TeamRequest)).ReverseMap();
            
        }
    }
}