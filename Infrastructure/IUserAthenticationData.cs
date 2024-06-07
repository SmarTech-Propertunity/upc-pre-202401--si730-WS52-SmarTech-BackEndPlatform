using _3_Data.Models;

namespace _3_Data;

public interface IUserAthenticationData
{
    public Task<User?> GetUserByCredentialsAsync(UserCredentials userCredentials);
    public Task<RefreshTokenRecord?> FindExistingRefreshTokenAsync(RefreshTokenModel refreshTokenModel);
    public Task<int> SaveRefreshTokenAsync(RefreshTokenRecord refreshTokenRecord);
}