using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
using System.Collections.Generic;

namespace ChildBehaviour.BLL.Abstracts.DecisionTable
{
    public interface IDecisionTable
    {
        void AddRule(BehaviourDto behaviour);
        List<IBaseResponse> Execute();
        ISymptom AddQuestionareSymptoms(SymptomDto symptom);
    }
}