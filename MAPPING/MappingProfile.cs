using AutoMapper;
using FitnessTracker.Models;
using FitnessTracker.DTOs;

namespace FitnessTracker.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WorkoutCreateDto, Workout>();
            CreateMap<Workout, WorkoutReadDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserProfile.UserName));
        }
    }
}
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Workout mappings
        CreateMap<Workout, WorkoutReadDto>();
        CreateMap<WorkoutCreateDto, Workout>();

        // WorkoutLog mappings
        CreateMap<WorkoutLog, WorkoutLogReadDto>();
        CreateMap<WorkoutLogCreateDto, WorkoutLog>();
    }
}
