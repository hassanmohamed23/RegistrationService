using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Common.Enums;

namespace Infrastructure.IRepository
{
    public interface IPrivacyPolicyAcceptanceRepository
    {
        Task<PrivacyPolicyAcceptance> GetByIdAsync(int id);
        Task<List<PrivacyPolicyAcceptance>> GetByUserIdAsync(int userId);
        Task AddAsync(PrivacyPolicyAcceptance acceptance);
        Task UpdateAsync(PrivacyPolicyAcceptance acceptance);
        Task<bool> HasAcceptedPolicyAsync(int userId, PolicyVersion policyVersion);
    }
}
