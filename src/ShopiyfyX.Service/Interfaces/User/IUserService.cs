using ShopiyfyX.Service.DTOs.UserDto;

namespace ShopiyfyX.Service.Interfaces.User;

public interface IUserService
{
    public Task<bool> RemoveAsync(long id);
    public Task<List<UserForResultDto>> GetAllAsync();
    public Task<UserForResultDto> GetByIdAsync(long id);
    public Task<UserForResultDto> UpdateAsync(UserForUpdateDto dto);
    public Task<UserForResultDto> CreateAsync(UserForCreationDto dto);
}
