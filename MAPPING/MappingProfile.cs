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
            CreateMap<WorkoutCreateDto, Workout>()
                .ReverseMap();  // Added ReverseMap() to allow mapping from WorkoutReadDto to Workout

            CreateMap<Workout, WorkoutReadDto>()
                .ReverseMap();  // ReverseMap to allow mapping back

            // WorkoutLog
            CreateMap<WorkoutLogCreateDto, WorkoutLog>()
                .ReverseMap();  // Added ReverseMap() for bidirectional mapping of WorkoutLog

            CreateMap<WorkoutLog, WorkoutLogReadDto>()
                .ForMember(dest => dest.WorkoutName, opt => opt.MapFrom(src => src.Workout.Name))
                .ReverseMap();  // ReverseMap to allow mapping back (e.g., to update WorkoutLog)

            // NutritionLog
            CreateMap<NutritionLogDto, NutritionLog>().ReverseMap(); // ReverseMap for NutritionLog

            // Goal
            CreateMap<GoalDto, Goal>().ReverseMap();  // ReverseMap for Goal
        }
    }
}
