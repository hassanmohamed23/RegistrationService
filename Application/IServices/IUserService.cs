using Application.Dtos;
using Domain.Models;
using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<RegistrationRequest> AddUserAsync(RegistrationRequest user);
        Task<RegistrationRequest> UpdateUserAsync(RegistrationRequest user);
        Task<RegistrationRequest> GetUserByUserIdAsync(int userId);
        Task<RegistrationRequest> GetUserByICNumberAsync(int icNumber);
        Task<ResponseModel> CreatePasscode(PasscodeRequest request);
        Task<ResponseModel> VerifyPasscode(PasscodeRequest request);
        Task<ResponseModel> SendOTPViaMobile(SendOTPRequest request);
        Task<ResponseModel> VerifyOTPViaMobile(VerifyOTPRequest request);
        Task<ResponseModel> SendOTPViaEmail(SendOTPRequest request);
        Task<ResponseModel> VerifyOTPViaEmail(VerifyOTPRequest request);
        Task<ResponseModel> ForgetPasscode(ForgetPasscodeRequest request);
    }
}
