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
    public class DiagnosisController : ControllerBase
    {

        private readonly IDiagnosisService _diagnosisService;

        public DiagnosisController(IDiagnosisService diagnosisService)
        {
            _diagnosisService = diagnosisService;
        }

        [HttpPost("[action]")]
        public async Task<IBaseResponse> Execute([FromBody] IEnumerable<SymptomDto> symptoms) =>
              await _diagnosisService.Execute(symptoms);

      

    }
}
