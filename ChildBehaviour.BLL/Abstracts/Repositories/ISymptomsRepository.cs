using ChildBehaviour.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Repositories
{
    public interface ISymptomsRepository
    {
        Task<IEnumerable<SymptomDto>> Get(int? id);
        Task<int> Add(SymptomDto symptom);
        Task<int> Update(SymptomDto symptom);
        Task RemoveExcludedRange(List<int> ids);

    }
}
