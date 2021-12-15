using ChildBehaviour.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Repositories
{
    public interface IRecommendationRespository
    {
        Task<int> Add(RecommendationDto recommendation);
        Task<IEnumerable<RecommendationDto>> Get(int? id);
        Task<int> Update(RecommendationDto recommendation);
    }
}