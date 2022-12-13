using AutoMapper;
using GdscBackend.Features.Articles;
using GdscBackend.Features.Contacts;
using GdscBackend.Features.Events;
using GdscBackend.Features.Faqs;
using GdscBackend.Features.Members;
using GdscBackend.Features.MenuItems;
using GdscBackend.Features.Pages;
using GdscBackend.Features.Redirects;
using GdscBackend.Features.Settings;
using GdscBackend.Features.Teams;
using GdscBackend.Features.Technologies;

namespace GdscBackend.Utils.Mappers;

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
        CreateMap(typeof(ContactModel), typeof(ContactRequest)).ReverseMap();
        CreateMap(typeof(RedirectModel), typeof(RedirectRequest)).ReverseMap();
        CreateMap(typeof(ArticleModel), typeof(ArticleRequest)).ReverseMap();
        CreateMap(typeof(ArticleModel), typeof(ArticleResponse)).ReverseMap();
    }
}