
﻿namespace ShopiyfyX.Service.Services.User;

﻿using ShopiyfyX.Domain.Entities;
using ShopiyfyX.Data.Repositories;
using ShopiyfyX.Service.Exceptions;
using ShopiyfyX.Data.IRepositories;
using ShopiyfyX.Service.DTOs.UserDto;
using ShopiyfyX.Service.Interfaces.User;

public class UserService : IUserService
{
    private readonly IRepository<User> userRepository = new Repository<User>();
    public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
    {

        var user = (await this.userRepository.SelectAllAsync()).FirstOrDefault(x => x.Email.ToLower() == dto.Email.ToLower());
        if (user is not null)
            throw new ShopifyXException(400, "User is already exist");
        var person = new User()
        {
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
            Id = response.Id,
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
        else
        {
            var result = await this.userRepository.DeleteAsync(datas.Id);
            return result;
        }
    }

    public async Task<UserForResultDto> UpdateAsync(UserForUpdateDto dto)
    {
        var data = await this.userRepository.SelectByIdAsync(dto.Id);
        if (data == null)
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
}