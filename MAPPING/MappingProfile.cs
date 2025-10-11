using AutoMapper;
using FitnessTracker.Models;
using FitnessTracker.DTOs;

namespace FitnessTracker.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //  Workout
            CreateMap<WorkoutCreateDto, Workout>().ReverseMap();
            CreateMap<Workout, WorkoutReadDto>().ReverseMap();

            //  WorkoutLog
            CreateMap<WorkoutLogCreateDto, WorkoutLog>().ReverseMap();
            CreateMap<WorkoutLog, WorkoutLogReadDto>()
                .ForMember(dest => dest.WorkoutName, opt => opt.MapFrom(src => src.Workout.Name))
                .ReverseMap();

            //  NutritionLog
            CreateMap<NutritionLogDto, NutritionLog>().ReverseMap();

            //  Goal
            CreateMap<GoalDto, Goal>().ReverseMap();

            //  UserProfile
            CreateMap<UserProfile, UserProfileDto>().ReverseMap();
        }
    }
}
