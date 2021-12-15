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
    public class SymptomsController : ControllerBase
    {
        private readonly ISymptomsService _symptomsService;

        public SymptomsController(ISymptomsService symptomsService)
        {
            _symptomsService = symptomsService;
        }

        [HttpPost("[action]")]
        public async Task<IBaseResponse> AddOrUpdate([FromBody] IEnumerable<SymptomDto> symptoms) =>
            await _symptomsService.AddOrUpdate(symptoms);


        [HttpGet("GetSymptoms")]
        public async Task<IResponse<IEnumerable<SymptomDto>>> GetSymptoms([FromQuery] int id)
           => await _symptomsService.Get(id);

    }
}
