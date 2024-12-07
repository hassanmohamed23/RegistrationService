using Application.Dtos;
using Application.IServices;
using AutoMapper;
using Domain.Models;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Common.Enums;

namespace Application.Services
{
    public class PrivacyPolicyAcceptanceService : IPrivacyPolicyAcceptanceService
    {
        private readonly IPrivacyPolicyAcceptanceRepository _repository;
        private readonly IMapper _mapper;


        public PrivacyPolicyAcceptanceService(IPrivacyPolicyAcceptanceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> AcceptPolicyAsync(int userId, PolicyVersion policyVersion)
        {
            var hasAccepted = await _repository.HasAcceptedPolicyAsync(userId, policyVersion);
            if (hasAccepted)
                return false;
            
            var acceptance = new PrivacyPolicyAcceptance
            {
                UserId = userId,
                PolicyVersion = policyVersion,
                AcceptedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(acceptance);

            return true;
        }

        public async Task<List<PrivacyPolicyAcceptanceDto>> GetUserAcceptancesAsync(int userId)
        {
            var privacyPolicyAcceptanceList = await _repository.GetByUserIdAsync(userId);
            return _mapper.Map<List<PrivacyPolicyAcceptanceDto>>(privacyPolicyAcceptanceList);
        }

        public async Task<bool> UpdatePolicyAcceptanceAsync(PrivacyPolicyAcceptance acceptance)
        {
            try
            {
                await _repository.UpdateAsync(acceptance);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
