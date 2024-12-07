using Domain.Models;
using Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Common.Enums;

namespace Infrastructure.Repository
{
    public class PrivacyPolicyAcceptanceRepository : IPrivacyPolicyAcceptanceRepository
    {
        private readonly ApplicationDbContext _context;

        public PrivacyPolicyAcceptanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PrivacyPolicyAcceptance> GetByIdAsync(int id)
        {
            var policy = await _context.PrivacyPolicyAcceptances
                        .Include(p => p.User)
                        .FirstOrDefaultAsync(p => p.Id == id);

            return policy;
        }

        public async Task<List<PrivacyPolicyAcceptance>> GetByUserIdAsync(int userId)
        {
            return await _context.PrivacyPolicyAcceptances
                .Include(p => p.User)
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(PrivacyPolicyAcceptance acceptance)
        {
            _context.PrivacyPolicyAcceptances.Add(acceptance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PrivacyPolicyAcceptance acceptance)
        {
            _context.PrivacyPolicyAcceptances.Update(acceptance);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasAcceptedPolicyAsync(int userId, PolicyVersion policyVersion)
        {
            return await _context.PrivacyPolicyAcceptances
                .AnyAsync(p => p.UserId == userId && p.PolicyVersion == policyVersion);
        }
    }

}

