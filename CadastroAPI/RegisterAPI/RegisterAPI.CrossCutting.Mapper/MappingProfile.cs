using AutoMapper;
using RegisterAPI.Entity.Entities;
using RegisterAPI.Model.Request.User;

namespace RegisterAPI.CrossCutting.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientRequest, Client>();
        }
    }
}
