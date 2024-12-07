using Domain.Models;
using Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users
                            .Include(u => u.PrivacyPolicyAcceptanceList)
                            .FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<User> GetUserByICNumberAsync(int icNumber)
        {
            var user = await _context.Users
                         .Include(u => u.PrivacyPolicyAcceptanceList)
                         .FirstOrDefaultAsync(u => u.ICNumber == icNumber);
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.PrivacyPolicyAcceptanceList)
                .ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdatePasscode(int userId, string passcode)
        {
            var user = await _context.Users.Where(up => up.Id.Equals(userId)).FirstOrDefaultAsync();

            if (user != null)
            {
                user.Passcode = passcode;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

    
        public async Task<bool> UpdateMobileVerificationCode(int userId, int verificationCode)
        {
            var user = await _context.Users.Where(up => up.Id.Equals(userId)).FirstOrDefaultAsync();

            if (user != null)
            {
                user.MobileVerificationCode = verificationCode;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateEmailVerificationCode(int userId, int verificationCode)
        {
            var user = await _context.Users.Where(up => up.Id.Equals(userId)).FirstOrDefaultAsync();

            if (user != null)
            {
                user.EmailVerificationCode = verificationCode;
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
