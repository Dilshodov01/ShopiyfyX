using ShopiyfyX.Domain.Entities;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Service.Exceptions;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Service.DTOs.UserDto;
using ShopiyfyX.Service.Interfaces.User;

namespace ShopiyfyX.Service;

public class UserService : IUserService
{
    private readonly IRepository<User> userRepository = new Repository<User>();
    private int _id;

    public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
    {

        var user = (await this.userRepository.SelectAllAsync())
            .FirstOrDefault(x => x.Email.ToLower() == dto.Email.ToLower());
        if (user is not null)
            throw new ShopifyXException(400, "User is already exist");

        await GenerateIdAsync();
        var person = new User()
        {
            Id = _id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Password = dto.Password,
            PhoneNumber = dto.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        var response = await this.userRepository.InsertAsync(person);

        var result = new UserForResultDto()
        {
            Id = _id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
        };

        return result;


    }

    public async Task<List<UserForResultDto>> GetAllAsync()
    {
        var data = await this.userRepository.SelectAllAsync();
        var result = new List<UserForResultDto>();
        foreach (var item in data)
        {
            var person = new UserForResultDto()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                PhoneNumber = item.PhoneNumber,
            };
            result.Add(person);
        }

        return result;
    }

    public async Task<UserForResultDto> GetByIdAsync(long id)
    {
        var data = await this.userRepository.SelectByIdAsync(id);
        if (data == null)
        {
            throw new ShopifyXException(404, "User is not found");
        }

        var person = new UserForResultDto()
        {
            Id = data.Id,
            FirstName = data.FirstName,
            LastName = data.LastName,
            PhoneNumber = data.PhoneNumber,

        };
        return person;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var datas = await this.userRepository.SelectByIdAsync(id);
        if (datas is null)
            throw new ShopifyXException(404, "User is not found");
        
        return await this.userRepository.DeleteAsync(datas.Id);
    }

    public async Task<UserForResultDto> UpdateAsync(UserForUpdateDto dto)
    {
        var user = await this.userRepository.SelectByIdAsync(dto.Id);
        if (user is null)
            throw new ShopifyXException(404, "User is not found");

        var person = new User()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            UpdatedAt = DateTime.UtcNow
        };

        await this.userRepository.UpdateAsync(person);

        var result = new UserForResultDto()
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,

        };

        return result;
    }

    // Generation Id
    public async Task<long> GenerateIdAsync()
    {
        var users = await userRepository.SelectAllAsync();
        var count = users.Count();
        if (count == 0)
        {
            _id = 1;
        }
        else
        {
            var user = users[count - 1];
            _id = (int)(++user.Id);
        }
        return _id;
    }
}