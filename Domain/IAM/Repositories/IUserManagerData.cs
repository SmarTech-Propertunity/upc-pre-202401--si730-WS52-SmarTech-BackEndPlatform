using _2_Domain.IAM.Models.Entities;
using _3_Data.Models;

namespace _3_Data;

public interface IUserManagerData
{
    public Task<UserInformation?> GetUserByIdAsync(int id);
}