using System.Net;
using AutoMapper;
using TodoList.Dtos;
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

        try
        {
            User? user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Success = false;
                response.Message = $"Error occurred: User with id '{id}' is not exists.";
            }

            response.Data = _mapper.Map<UserGetDto>(user);
        }
        catch (Exception ex)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Success = false;
            response.Message = $"Error occurred: {ex.Message}";
        }

        return response;
    }

    public Task<ServiceResponse<UserGetDto>> GetByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }
}