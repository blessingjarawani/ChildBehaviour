using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.Abstracts.Services;
using ChildBehaviour.BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildBehaviour.RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("[action]")]
        public async Task<IBaseResponse> AddNew([FromBody] UserDto user)
             => await _usersService.AddNew(user);

        [HttpGet]
        public async Task<IResponse<IEnumerable<PupilDto>>> GetParentPupils([FromQuery] int userId)
             => await _usersService.GetParentPupils(userId);

        [HttpPost("[action]")]
        public async Task<IBaseResponse> UpdateUser([FromBody] UserDto user) => await _usersService.UpdateUser(user);

        [HttpPost("[action]")]
        public async Task<IResponse<UserDto>> Login([FromBody] UserDto user) => await _usersService.Login(user);
    }
}
