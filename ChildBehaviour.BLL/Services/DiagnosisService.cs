using ChildBehaviour.BLL.Abstracts.DecisionTable;
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
    public class DiagnosisService : IDiagnosisService
    {
        private readonly IDecisionTable _decisionTable;
        private readonly IBehaviourRespository _behaviourRepository;
        private readonly IPupilRespository _pupilRespository;

        public DiagnosisService(IDecisionTable decisionTable, IBehaviourRespository behaviourRepository, IPupilRespository pupilRespository)
        {
            _decisionTable = decisionTable;
            _behaviourRepository = behaviourRepository;
            _pupilRespository = pupilRespository;
        }


        private async Task<bool> AddBehaviours()
        {
            try
            {
                var result = await _behaviourRepository.Get();
                if (result.Any())
                {
                    foreach (var behaviour in result)
                    {
                        _decisionTable.AddRule(behaviour);
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> AddQuestionnareSymptoms(List<SymptomDto> symptoms)
        {
            try
            {
                if (symptoms?.Any() ?? false)
                {
                    foreach (var symptom in symptoms)
                    {
                        _decisionTable.AddQuestionareSymptoms(symptom);
                    }
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IBaseResponse> Execute(List<SymptomDto> symptoms)
        {
            try
            {
                var result = await AddQuestionnareSymptoms(symptoms);
                if (!result)
                {
                    return BaseResponse.CreateFailure("Failure on Pasing Symptoms");
                }
                var addBehavioursResult = await AddBehaviours();
                if (!addBehavioursResult)
                {
                    return BaseResponse.CreateFailure("Failure on Pasing Rules");
                }
                var diagnosisResult = _decisionTable.Execute();
                if (diagnosisResult.Any(t => t.Success))
                {
                    return BaseResponse.CreateSuccess("Found");
                }

                return BaseResponse.CreateFailure("No Matching Behaviour Found Please try to Select other Conditions");

            }
            catch (Exception ex)
            {

                return BaseResponse.CreateFailure(ex.GetBaseException().Message);
            }
        }

    }
}
