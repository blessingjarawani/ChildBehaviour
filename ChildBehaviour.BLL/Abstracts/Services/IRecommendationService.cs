using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Services
{
    public interface IRecommendationService
    {
        Task<IBaseResponse> AddOrUpdate(IEnumerable<RecommendationDto> recommendations);
        Task<IBaseResponse> DeleteRange(IEnumerable<int> ids);
    }
}