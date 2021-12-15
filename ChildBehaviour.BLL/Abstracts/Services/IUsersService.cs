using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Services
{
    public interface IUsersService
    {
        Task<IBaseResponse> AddNew(UserDto user);
        Task<IResponse<IEnumerable<PupilDto>>> GetParentPupils(int userId);
        Task<IResponse<UserDto>> Login(LoginDto user);
        Task<IBaseResponse> UpdateUser(UserDto user);
    }
}