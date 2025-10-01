using AutoMapper;
using FitnessTracker.Models;
using FitnessTracker.DTOs;

namespace FitnessTracker.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


            CreateMap<Workout, WorkoutReadDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserProfile!.UserName));

            CreateMap<WorkoutCreateDto, Workout>();
        }
    }
}
