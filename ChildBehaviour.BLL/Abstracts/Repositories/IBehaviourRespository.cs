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
        Task<IEnumerable<BehaviourDto>> Get(int? id);
    }
}
