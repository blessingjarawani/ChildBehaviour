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
    public class SymptomsService : ISymptomsService
    {
        private readonly ISymptomsRepository _symptomsRepository;

        public SymptomsService(ISymptomsRepository symptomsRepository)
        {
            _symptomsRepository = symptomsRepository;
        }

        public async Task<IBaseResponse> AddOrUpdate(IEnumerable<SymptomDto> symptoms)
        {
            try
            {
                var result = 0;
                if (symptoms?.Any() ?? false)
                {
                    foreach (var symptom in symptoms)
                    {
                        if (symptom.Id > 0)
                        {
                            result = await _symptomsRepository.Update(symptom);
                        }
                        else
                        {
                            result = await _symptomsRepository.Add(symptom);
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

        public async Task<IBaseResponse> DeleteRange(IEnumerable<int> ids)
        {
            try
            {
                if (ids?.Any() ?? false)
                {
                    await _symptomsRepository.DeleteRange(ids);
                    return BaseResponse.CreateSuccess("Deleted Sucessfully");
                }
                return BaseResponse.CreateFailure("No Ids Passed");

            }
            catch (Exception ex)
            {
                return BaseResponse.CreateFailure(ex.GetBaseException().Message);
            }
        }
    }
}
