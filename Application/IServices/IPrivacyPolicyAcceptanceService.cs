using Application.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Common.Enums;

namespace Application.IServices
{
    public interface IPrivacyPolicyAcceptanceService
    {
        Task<bool> AcceptPolicyAsync(int userId, PolicyVersion policyVersion);
        Task<List<PrivacyPolicyAcceptanceDto>> GetUserAcceptancesAsync(int userId);
        Task<bool> UpdatePolicyAcceptanceAsync(PrivacyPolicyAcceptance acceptance);
    }
}
