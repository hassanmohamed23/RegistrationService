using Application.Dtos;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationRequest, User>()
                .ForMember(dest => dest.MobileVerificationCode, opt => opt.Ignore())
                .ForMember(dest => dest.EmailVerificationCode, opt => opt.Ignore())
                .ForMember(dest => dest.PrivacyPolicyAcceptanceList, opt => opt.Ignore());

            CreateMap<PrivacyPolicyAcceptance, PrivacyPolicyAcceptanceDto>().ReverseMap();

        }
    }
}
