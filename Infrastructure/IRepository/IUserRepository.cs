using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByICNumberAsync(int icNumber);
        Task<List<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);

        Task<bool> UpdatePasscode(int userId, string passcode);
        Task<bool> UpdateMobileVerificationCode(int userId, int verificationCode);
        Task<bool> UpdateEmailVerificationCode(int userId, int verificationCode);
    }
}
