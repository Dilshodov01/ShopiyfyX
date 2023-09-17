using ShopiyfyX.Service.DTOs;

namespace ShopiyfyX.Service.Interfaces.User;

public interface IUserInterface
{
    public Task<bool> RemoveAsync(long id);
    public Task<List<UserForResultDto>> GetAllAsync();
    public Task<UserForResultDto> GetByIdAsync(long id);
    public Task<UserForResultDto> UpdateAsync(UserForUpdateDto dto);
    public Task<UserForResultDto> CreateAsync(UserForCreationDto dto);
}
