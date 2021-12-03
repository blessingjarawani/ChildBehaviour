using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Services
{
    public interface IPupilService
    {
        Task<IBaseResponse> AddAssessment(ChildAssessmentDto childAssessment);
        Task<IBaseResponse> AddOrUpdate(IEnumerable<PupilDto> pupils);
        Task<IResponse<IEnumerable<ChildAssessmentDto>>> GetPupilAssesments(int? id);
    }
}