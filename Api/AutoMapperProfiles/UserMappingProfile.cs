using AutoMapper;
using LearningASP.DTO;
using LearningASP.Model;

namespace LearningASP.AutoMapperProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}