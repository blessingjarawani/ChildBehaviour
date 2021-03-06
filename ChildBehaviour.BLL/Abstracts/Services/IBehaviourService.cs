using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Services
{
    public interface IBehaviourService
    {
        Task<IBaseResponse> AddOrUpdate(IEnumerable<BehaviourDto> behaviour);
        Task<IResponse<IEnumerable<BehaviourDto>>> GetBehaviourSymptoms(int id);
        Task<IBaseResponse> AddBehaviourRecommendations(BehaviourDto behaviour);
        Task<IBaseResponse> AddBehaviourSymptoms(BehaviourDto behaviour);
        Task<IResponse<IEnumerable<BehaviourDto>>> GetBehaviours(int id);
        Task<IResponse<IEnumerable<BehaviourDto>>> GetBehaviourRecommendations(int id);
    }
}
