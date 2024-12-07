using Application.Dtos;
using Application.Helpers;
using Application.IServices;
using AutoMapper;
using Domain.Models;
using Infrastructure.IRepository;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, INotificationService notificationService, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _notificationService= notificationService;
        }

        public async Task<RegistrationRequest> AddUserAsync(RegistrationRequest userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _userRepository.AddUserAsync(user);

            return userDto;
        }
        public async Task<RegistrationRequest> UpdateUserAsync(RegistrationRequest userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.UpdateUserAsync(user);

            return userDto;
        }
        public async Task<RegistrationRequest> GetUserByUserIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return _mapper.Map<RegistrationRequest>(user);
        }
        public async Task<RegistrationRequest> GetUserByICNumberAsync(int icNumber)
        {
            var user = await _userRepository.GetUserByICNumberAsync(icNumber);
            return _mapper.Map<RegistrationRequest>(user);
        }

        public async Task<ResponseModel> VerifyPasscode(PasscodeRequest request)
        {
            var result= new ResponseModel();
            var userProfile = await _userRepository.GetUserByIdAsync(request.UserId);

            if (userProfile is null)
            {
                result.IsSuccess = false;
                result.Message = "User not found";
                return result;
            }

            var encryptedPasscode = EncryptionHelper.EncryptString(request.Passcode.ToString());

            if (encryptedPasscode != userProfile.Passcode)
            {
                result.IsSuccess = false;
                result.Message = "Invalid Passcode";
                return result;
            }

            result.IsSuccess = true;
            result.Message = "Passcode is valid";
            return result;
        }

        public async Task<ResponseModel> CreatePasscode( PasscodeRequest request)
        {
            var result = new ResponseModel();

            var userProfile = await _userRepository.GetUserByIdAsync(request.UserId);
            if (userProfile is null)
            {
                result.IsSuccess = false;
                result.Message = "User not found";
                return result;
            }

            if (!string.IsNullOrEmpty(userProfile.Passcode))
            {

                result.IsSuccess = false;
                result.Message = "Failed to create: the passcode already exists";
                return result;
            }

            var passcodeEncrypted = EncryptionHelper.EncryptString(request.Passcode.ToString());
            var isPasscodeUpdated = await _userRepository.UpdatePasscode(request.UserId, passcodeEncrypted);

            if (!isPasscodeUpdated)
            {
                result.IsSuccess = false;
                result.Message = "Failed to create passcode";
                return result;
            }
            result.IsSuccess = true;
            result.Message = "Passcode created successfully";
            return result;
        }

        public async Task<ResponseModel> SendOTPViaMobile(SendOTPRequest request)
        {
            var result = new ResponseModel();

            var userProfile = await _userRepository.GetUserByIdAsync(request.UserId);

            if (userProfile is null)
            {
                result.IsSuccess = false;
                result.Message = "User not found";
                return result;
            }

            var OTPCode = GenerateRandomFourDigitNumber();
            await _notificationService.SendSms(userProfile.MobileNumber, OTPCode.ToString());
            await _userRepository.UpdateMobileVerificationCode(request.UserId, OTPCode);

            result.IsSuccess = true;
            result.Message = "The OTP has been send successfully";
            return result;

        }
        public async Task<ResponseModel> VerifyOTPViaMobile(VerifyOTPRequest request)
        {
            var result = new ResponseModel();

            var userProfile = await _userRepository.GetUserByIdAsync(request.UserId);

            if (userProfile is null)
            {
                result.IsSuccess = false;
                result.Message = "User not found";
                return result;
            }

            if (userProfile.MobileVerificationCode != request.OTP)
            {
                result.IsSuccess = false;
                result.Message = "The OTP is incorrect";
                return result;

            }

            result.IsSuccess = true;
            result.Message = "The OTP has been successfully verified";
            return result;
        }

        public async Task<ResponseModel> SendOTPViaEmail(SendOTPRequest request)
        {
            var result = new ResponseModel();

            var userProfile = await _userRepository.GetUserByIdAsync(request.UserId);

            if (userProfile is null)
            {
                result.IsSuccess = false;
                result.Message = "User not found";
                return result;
            }

            var OTPCode = GenerateRandomFourDigitNumber();
            await _notificationService.SendEmailAsync(userProfile.Email, OTPCode.ToString());
            await _userRepository.UpdateEmailVerificationCode(request.UserId, OTPCode);

            result.IsSuccess = true;
            result.Message = "The OTP has been send successfully";
            return result;

        }

        public async Task<ResponseModel> VerifyOTPViaEmail(VerifyOTPRequest request)
        {
            var result = new ResponseModel();

            var userProfile = await _userRepository.GetUserByIdAsync(request.UserId);

            if (userProfile is null)
            {
                result.IsSuccess = false;
                result.Message = "User not found";
                return result;
            }

            if (userProfile.EmailVerificationCode != request.OTP)
            {
                result.IsSuccess = false;
                result.Message = "The OTP is incorrect";
                return result;

            }

            result.IsSuccess = true;
            result.Message = "The OTP has been successfully verified";
            return result;
        }
        public async Task<ResponseModel> ForgetPasscode(ForgetPasscodeRequest request)
        {
            var result = new ResponseModel();

            var userProfile = await _userRepository.GetUserByIdAsync(request.UserId);
            if (userProfile is null)
            {
                result.IsSuccess = false;
                result.Message = "User not found";
                return result;
            }


            var passcodeEncrypted = EncryptionHelper.EncryptString(request.NewPasscode.ToString());
            var isPasscodeUpdated = await _userRepository.UpdatePasscode(request.UserId, passcodeEncrypted);

            if (!isPasscodeUpdated)
            {
                result.IsSuccess = false;
                result.Message = "Failed to update passcode";
                return result;
            }

            result.IsSuccess = true;
            result.Message = "Passcode is updated successfully";
            return result;
        }

        private int GenerateRandomFourDigitNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000); 
            return randomNumber;
        }
    }
}
