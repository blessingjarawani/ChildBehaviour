using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Services
{
    public interface ISymptomsService
    {
        Task<IBaseResponse> AddOrUpdate(IEnumerable<SymptomDto> symptoms);
        Task<IBaseResponse> DeleteRange(IEnumerable<int> ids);
    }
}