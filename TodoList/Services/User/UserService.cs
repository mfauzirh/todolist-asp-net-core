using System.Net;
using AutoMapper;
using TodoList.Dtos;
using TodoList.Exceptions;
using TodoList.Models;
using TodoList.Repositories;

namespace TodoList.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<UserGetDto>> GetByIdAsync(int id)
    {
        var response = new ServiceResponse<UserGetDto>();

        User? user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            throw new NotFoundException($"Error occurred: User with id '{id}' is not exists.");
        }

        response.Data = _mapper.Map<UserGetDto>(user);

        return response;
    }

    public Task<ServiceResponse<UserGetDto>> GetByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }
}