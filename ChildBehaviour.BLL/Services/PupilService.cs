using ChildBehaviour.BLL.Abstracts.Repositories;
using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.Abstracts.Services;
using ChildBehaviour.BLL.DTOs;
using ChildBehaviour.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Services
{
    public class PupilService : IPupilService
    {
        private readonly IPupilRespository _pupilRepository;
        private const string DB_SAVE_ERROR = "Failed to Save to DB";
        public PupilService(IPupilRespository pupilRepository)
        {
            _pupilRepository = pupilRepository;
        }

        public async Task<IBaseResponse> AddOrUpdate(IEnumerable<PupilDto> pupils)
        {
            var result = 0;
            try
            {
                if (pupils?.Any() ?? false)
                {
                    foreach (var pupil in pupils)
                    {
                        if (pupil.Id > 0)
                        {
                            result = await _pupilRepository.Update(pupil);
                        }
                        else
                        {
                            result = await _pupilRepository.Add(pupil);
                        }
                    }

                    return BaseResponse.CreateSuccess("Added Successfully");

                }
                return BaseResponse.CreateFailure("Passes null object");

            }
            catch (Exception ex)
            {

                return BaseResponse.CreateFailure(ex.GetBaseException().Message);
            }
        }

        public async Task<IBaseResponse> AddAssessment(ChildAssessmentDto childAssessment)
        {
            try
            {
                if (childAssessment != null)
                {
                    var result = await _pupilRepository.AddAssessment(childAssessment);
                    return result > 0 ? BaseResponse.CreateSuccess("Added Successfully") : BaseResponse.CreateFailure(DB_SAVE_ERROR);

                }
                return BaseResponse.CreateFailure("Invalid Object Passed");

            }
            catch (Exception ex)
            {

                return BaseResponse.CreateFailure(ex.GetBaseException().Message);
            }

        }

        public async Task<IResponse<IEnumerable<ChildAssessmentDto>>> GetPupilAssesments(int? id)
        {
            try
            {
                var result = await _pupilRepository.GetPupilAssesments(id);
                return Response<IEnumerable<ChildAssessmentDto>>.CreateSuccess(result);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<ChildAssessmentDto>>.CreateFailure(ex.GetBaseException().Message);
            }
        }
    }
}
