using ChildBehaviour.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Repositories
{
    public interface IBehaviourRespository
    {
        Task<IEnumerable<BehaviourDto>> Get(int? id = null);
        Task<int> Add(BehaviourDto behaviourDto);
        Task<List<BehaviourDto>> GetBehaviourRecommendations(int id);
        Task<int> Update(BehaviourDto behaviourDto);
        Task AddBehaviourRecommendations(BehaviourDto behaviour);
        Task<List<BehaviourDto>> GetBehaviourSymptoms(int id);
        Task AddBehaviourSymptoms(BehaviourDto behaviour);
        Task<IEnumerable<SymptomDto>> GetExcludedBehaviourSymptoms(IEnumerable<int> symptomsId);
        Task<IEnumerable<RecommendationDto>> GetExcludedBehaviourRecommendations(IEnumerable<int> recommendationdIds);


    }
}
