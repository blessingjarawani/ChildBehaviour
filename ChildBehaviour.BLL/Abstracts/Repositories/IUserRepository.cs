using ChildBehaviour.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsUserExists(string userName);
        Task<int> Add(UserDto user);
        Task<int> Update(UserDto user);
        Task<IEnumerable<PupilDto>> GetParentPupils(int userId);
        public Task<UserDto> Login(LoginDto user);
    }
}
