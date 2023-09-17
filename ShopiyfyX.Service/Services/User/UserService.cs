using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Domain.Entities;
using ShopiyfyX.Service.DTOs.UserDto;
using ShopiyfyX.Service.Interfaces.User;

namespace ShopiyfyX.Service.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> productRepository = new Repository<User>();

    public Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserForResultDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserForResultDto> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<UserForUpdateDto> UpdateAsync(UserForUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
