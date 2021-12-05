using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Services
{
    public interface IDiagnosisService
    {
        Task<IBaseResponse> Execute(IEnumerable<SymptomDto> symptoms);
    }
}