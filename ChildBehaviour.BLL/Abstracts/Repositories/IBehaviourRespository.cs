﻿using ChildBehaviour.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Repositories
{
    public interface IBehaviourRespository
    {
        Task<IEnumerable<BehaviourDto>> Get(int? id);
        Task<int> Add(BehaviourDto behaviourDto);
        Task<IEnumerable<BehaviourDto>> GetBehaviourRecommendations(int id);
        Task<int> Update(BehaviourDto behaviourDto);
        Task AddBehaviourRecommendations(BehaviourDto behaviour);
        Task<List<BehaviourDto>> GetBehaviourSymptoms(int id);
        Task AddBehaviourSymptoms(BehaviourDto behaviour);
       
    }
}
