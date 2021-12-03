﻿using ChildBehaviour.BLL.Abstracts.Repositories;
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
    public class BehaviourService : IBehaviourService
    {
        private readonly IBehaviourRespository _behaviourRepository;
        private const string DB_SAVE_ERROR = "Failed to Save to DB";
        public BehaviourService(IBehaviourRespository behaviourRepository)
        {
            _behaviourRepository = behaviourRepository;
        }


        public async Task<IBaseResponse> AddOrUpdate(IEnumerable<BehaviourDto> behaviours)
        {
            try
            {
                var result = 0;
                if (behaviours?.Any() ?? false)
                {
                    foreach (var behaviour in behaviours)
                    {
                        if (behaviour.Id > 0)
                        {
                            result = await _behaviourRepository.Update(behaviour);
                        }
                        else
                        {
                            result = await _behaviourRepository.Add(behaviour);
                        }
                        return result > 0 ? BaseResponse.CreateSuccess("Added Successfully") : BaseResponse.CreateFailure(DB_SAVE_ERROR);
                    }

                }
                return BaseResponse.CreateFailure("Passes null object");
            }
            catch (Exception ex)
            {

                return BaseResponse.CreateFailure(ex.GetBaseException().Message);
            }

        }

        public async Task<IResponse<IEnumerable<BehaviourDto>>> GetBehaviourSymptoms(int id)
        {
            try
            {
                var result = await _behaviourRepository.GetBehaviourSymptoms(id);
                return Response<IEnumerable<BehaviourDto>>.CreateSuccess(result);
            }
            catch (Exception ex)
            {
                return Response<IEnumerable<BehaviourDto>>.CreateFailure(ex.GetBaseException().Message);
            }
        }

        public async Task<IBaseResponse> AddBehaviourRecommendations(BehaviourDto behaviour)
        {
            try
            {
                if (behaviour != null && behaviour.Id > 0 && ((behaviour.Recommendations?.Any()) ?? false))
                {
                    await _behaviourRepository.AddBehaviourRecommendations(behaviour);
                    await _behaviourRepository.RemoveExcludedRangeRecommendations(behaviour.Recommendations.Select(t => t.Id), behaviour.Id);
                    BaseResponse.CreateSuccess("Added Successfully");

                }
                return BaseResponse.CreateFailure("Ivalid Object Passed");
            }
            catch (Exception ex)
            {

                return BaseResponse.CreateFailure(ex.GetBaseException().Message);
            }
        }

        public async Task<IBaseResponse> AddBehaviourSymptoms(BehaviourDto behaviour)
        {
            try
            {
                if (behaviour != null && behaviour.Id > 0 && ((behaviour.Symptoms?.Any()) ?? false))
                {
                    await _behaviourRepository.AddBehaviourRecommendations(behaviour);
                    await _behaviourRepository.RemoveExcludedRangeSymptoms(behaviour.Symptoms.Select(t => t.Id), behaviour.Id);
                    BaseResponse.CreateSuccess("Added Successfully");

                }
                return BaseResponse.CreateFailure("Ivalid Object Passed");
            }
            catch (Exception ex)
            {

                return BaseResponse.CreateFailure(ex.GetBaseException().Message);
            }
        }


    }
}
