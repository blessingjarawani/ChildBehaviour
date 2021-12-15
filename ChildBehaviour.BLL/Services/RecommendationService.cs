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
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRespository _recommendationRepository;

        public RecommendationService(IRecommendationRespository recommendationRepository)
        {
            _recommendationRepository = recommendationRepository;
        }
        public async Task<IBaseResponse> AddOrUpdate(IEnumerable<RecommendationDto> recommendations)
        {
            try
            {
                var result = 0;
                if (recommendations?.Any() ?? false)
                {
                    foreach (var recommendation in recommendations)
                    {
                        if (recommendation.Id > 0)
                        {
                            result = await _recommendationRepository.Update(recommendation);
                        }
                        else
                        {
                            result = await _recommendationRepository.Add(recommendation);
                        }

                    }
                    return BaseResponse.CreateSuccess("Added Successfully");

                }
                return BaseResponse.CreateFailure("Invalid Object Passed");

            }
            catch (Exception ex)
            {
                return BaseResponse.CreateFailure(ex.GetBaseException().Message);
            }
        }

      
    }


}

