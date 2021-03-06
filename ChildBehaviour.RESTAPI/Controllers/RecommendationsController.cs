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
    public class RecommendationsController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationsController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }
        [HttpPost("[action]")]
        public async Task<IBaseResponse> AddOrUpdate([FromBody] IEnumerable<RecommendationDto> recommendations)
            => await _recommendationService.AddOrUpdate(recommendations);

        [HttpGet("GetRecommendations")]
        public async Task<IResponse<IEnumerable<RecommendationDto>>> GetRecommendations([FromQuery] int id)
           => await _recommendationService.Get(id);

    }
}
