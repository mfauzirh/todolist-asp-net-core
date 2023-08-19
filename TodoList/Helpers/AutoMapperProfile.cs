using AutoMapper;
using TodoList.Dtos;
using TodoList.Models;

namespace TodoList.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserGetDto>().ReverseMap();
    }
}