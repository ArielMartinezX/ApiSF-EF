using Application.DTOs;
using AutoMapper;
using Models;

namespace ApiStreetFighter.Profiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile() 
        {
            CreateMap<Fighter, EntityDTO>();
            CreateMap<CreateUpdateEntityDTO, Fighter>();

        }
    }
}
