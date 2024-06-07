using _3_Data.Models;

namespace _2_Domain;

public interface IUserRegisterDomain
{
    public Task<int> RegisterUserAsync(User data);
}