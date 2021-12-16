using ChildBehaviour.BLL.Abstracts.Repositories;
using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.Abstracts.Services;
using ChildBehaviour.BLL.DTOs;
using ChildBehaviour.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
      
        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IResponse<IEnumerable<PupilDto>>> GetParentPupils(int userId)
        {
            try
            {
                var result = await _userRepository.GetParentPupils(userId);
                return Response<IEnumerable<PupilDto>>.CreateSuccess(result);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<PupilDto>>.CreateFailure(ex.GetBaseException().Message);
            }
        }

        public async Task<IBaseResponse> AddNew(UserDto user)
        {
            try
            {
                if (user != null && user.IsValid)
                {
                    var isUserExist = await _userRepository.IsUserExists(user.UserName);
                    if (!isUserExist)
                    {
                        await _userRepository.Add(user);
                        return BaseResponse.CreateSuccess("User Successfully Created");
                    }
                    return BaseResponse.CreateFailure("UserName Already Exists");
                }
                return BaseResponse.CreateFailure("Invalid Object Passed");

            }
            catch (Exception ex)
            {
                return BaseResponse.CreateFailure(ex.GetBaseException().Message);
            }
        }

        public async Task<IBaseResponse> UpdateUser(UserDto user)
        {
            try
            {
                if (user != null && user.IsValid)
                {
                    var isUserExist = await _userRepository.IsUserExists(user.UserName);
                    if (isUserExist)
                    {
                        await _userRepository.Update(user);
                        return BaseResponse.CreateSuccess("User Successfully Updated");
                    }
                    return BaseResponse.CreateFailure("UserName Not Found");
                }
                return BaseResponse.CreateFailure("Invalid Object Passed");

            }
            catch (Exception ex)
            {
                return BaseResponse.CreateFailure(ex.GetBaseException().Message);
            }
        }
        public async Task<IResponse<UserDto>> Login(LoginDto user)
        {
            try
            {
                if (user != null && user.IsValid)
                {

                    var result = await _userRepository.Login(user);
                    return result != null ?
                                    Response<UserDto>.CreateSuccess(result) : Response<UserDto>.CreateFailure("Invalid Login");
                }
                return Response<UserDto>.CreateFailure("Invalid Object Paased");

            }

            catch (Exception ex)
            {
                return Response<UserDto>.CreateFailure(ex.GetBaseException().Message);
            }
        }
    }
}
