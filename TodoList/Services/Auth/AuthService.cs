using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TodoList.Dtos;
using TodoList.Exceptions;
using TodoList.Models;
using TodoList.Repositories;

namespace TodoList.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<(ServiceResponse<UserGetDto>, string token)> Login(UserLoginDto loginUser)
    {
        var response = new ServiceResponse<UserGetDto>();

        User? user = await _userRepository.GetByUserNameAsync(loginUser.UserName);

        if (user is null)
        {
            throw new NotFoundException(
                $"Error occurred: User with username '{loginUser.UserName}' is not exists");
        }

        if (!VerifyPasswordHash(loginUser.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new UnauthorizedException(
                $"Error occurred: wrong password for user with username '{loginUser.UserName}'");
        }

        string token = CreateToken(user);
        response.Data = _mapper.Map<UserGetDto>(user);

        return (response, token);
    }

    public async Task<ServiceResponse<UserGetDto>> Register(UserRegisterDto newUser)
    {
        var response = new ServiceResponse<UserGetDto>();

        if (await UserExists(newUser.UserName))
        {
            throw new ConflictException(
                $"Error occurred: User with username '{newUser.UserName}' is already exists"
            );
        }

        CreatePasswordHash(newUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

        User user = new()
        {
            UserName = newUser.UserName,
            FullName = newUser.FullName,
            Email = newUser.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        User registeredUser = await _userRepository.AddAsync(user);
        response.Data = _mapper.Map<UserGetDto>(registeredUser);

        return response;
    }

    public async Task<bool> UserExists(string UserName)
    {
        return await _userRepository.GetByUserNameAsync(UserName) is not null;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);

        byte[] computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        return computeHash.SequenceEqual(passwordHash);
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        string? appSettingsToken = _configuration.GetSection("AppSettings:Token").Value ?? throw new Exception("AppSettings Token is null");

        SymmetricSecurityKey key = new(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}