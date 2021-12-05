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
    public class BehaviourController : ControllerBase
    {
        private readonly IBehaviourService _behaviourService;

        public BehaviourController(IBehaviourService behaviourService)
        {
            _behaviourService = behaviourService;
        }

        [HttpPost("[action]")]
        public async Task<IBaseResponse> AddOrUpdateBehaviour([FromBody] IEnumerable<BehaviourDto> behaviours)
            => await _behaviourService.AddOrUpdate(behaviours);

        [HttpGet("GetBehaviours")]
        public async Task<IResponse<IEnumerable<BehaviourDto>>> GetBehaviours([FromQuery] int id)
            => await _behaviourService.GetBehaviours(id);

        [HttpGet("GetBehaviourSymptoms")]
        public async Task<IResponse<IEnumerable<BehaviourDto>>> GetBehaviourSymptoms([FromQuery] int id)
            => await _behaviourService.GetBehaviourSymptoms(id);

        [HttpGet("GetBehaviourRecommendations")]
        public async Task<IResponse<IEnumerable<BehaviourDto>>> GetBehaviourRecommendations([FromQuery] int id)
          => await _behaviourService.GetBehaviourRecommendations(id);

        
        [HttpPost("[action]")]
        public async Task<IBaseResponse> AddBehaviourSymptoms([FromBody] BehaviourDto behaviour)
            => await _behaviourService.AddBehaviourSymptoms(behaviour);

        [HttpPost("[action]")]
        public async Task<IBaseResponse> AddBehaviourRecommendations([FromBody] BehaviourDto behaviour)
           => await _behaviourService.AddBehaviourRecommendations(behaviour);

    }
}
