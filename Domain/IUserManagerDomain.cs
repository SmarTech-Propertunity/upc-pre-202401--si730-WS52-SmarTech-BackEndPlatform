using _3_Data.Models;

namespace _2_Domain;

public interface IUserManagerDomain
{
    public Task<UserInformation?> GetUserByIdAsync(int id);
}