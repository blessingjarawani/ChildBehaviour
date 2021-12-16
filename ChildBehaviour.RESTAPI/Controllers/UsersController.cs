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
        private readonly IPupilService _pupilService;

        public UsersController(IUsersService usersService, IPupilService pupilService)
        {
            _usersService = usersService;
            _pupilService = pupilService;
        }

        [HttpPost("[action]")]
        public async Task<IBaseResponse> AddNew([FromBody] UserDto user)
             => await _usersService.AddNew(user);

        [HttpGet("GetParentPupils")]
        public async Task<IResponse<IEnumerable<PupilDto>>> GetParentPupils([FromQuery] int id)
             => await _usersService.GetParentPupils(id);

        [HttpPost("[action]")]
        public async Task<IBaseResponse> UpdateUser([FromBody] UserDto user) => await _usersService.UpdateUser(user);

        [HttpPost("[action]")]
        public async Task<IResponse<UserDto>> Login([FromBody] LoginDto user) => await _usersService.Login(user);

        [HttpPost("[action]")]
        public async Task<IBaseResponse> AddOrUpdatePupils([FromBody] IEnumerable<PupilDto> pupils)
            => await _pupilService.AddOrUpdate(pupils);
    }
}
