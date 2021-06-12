using AutoMapper;
using GdscBackend.Auth;

namespace GdscBackend.RequestModels
{
    public class Request : Profile
    {
        public Request()
        {
            CreateMap<Request,User>();
        }
    }
}