using ChildBehaviour.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Repositories
{
    public interface IPupilRespository
    {
        Task<int> Add(PupilDto pupil);
        Task<int> Update(PupilDto pupil);
        Task<int> AddAssessment(ChildAssessmentDto childAssessment);
        Task<IEnumerable<ChildAssessmentDto>> GetPupilAssesments(int? id);
    }
}
