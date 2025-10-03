using AutoMapper;
using FitnessTracker.Models;
using FitnessTracker.DTOs;

namespace FitnessTracker.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Workout
            CreateMap<WorkoutCreateDto, Workout>();
            CreateMap<Workout, WorkoutReadDto>();

            // WorkoutLog
            CreateMap<WorkoutLogCreateDto, WorkoutLog>();
            CreateMap<WorkoutLog, WorkoutLogReadDto>()
                .ForMember(dest => dest.WorkoutName, opt => opt.MapFrom(src => src.Workout.Name));

            // NutritionLog
            CreateMap<NutritionLogDto, NutritionLog>().ReverseMap();

            // Goal
            CreateMap<GoalDto, Goal>().ReverseMap();
        }
    }
}
