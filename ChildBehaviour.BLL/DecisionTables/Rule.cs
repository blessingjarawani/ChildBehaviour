using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
using ChildBehaviour.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.DecisionTables
{
    public class Rule
    {

        private readonly BehaviourDto _behavior;
        private const int MIN_PERCENTAGE = 90;
        public Rule(BehaviourDto behaviour)
        {
            _behavior = behaviour;
        }
        public IBaseResponse Execute(IEnumerable<Symptom> symptoms)
        {
            if (!AllConditionsMet())
            {
                return BaseResponse.CreateFailure($"No Symptoms Found on {_behavior.Name}");
            }
            if (!(symptoms?.Any() ?? false))
            {
                return BaseResponse.CreateFailure($"No Symptoms Selected");
            }
            var matchedSymptomsCount = _behavior.Symptoms.Where(x => symptoms.Any(t => t._Symptom.Id == x.Id && t._Symptom.IsActive == x.IsActive)).Count();
            var behaviorSymptomsCount = _behavior?.Symptoms?.Count() > 0 ? _behavior.Symptoms.Count() : 1;
            if (behaviorSymptomsCount == 0)
            {
                return BaseResponse.CreateFailure("Does Not Match");
            }
            var percentageMatch = (matchedSymptomsCount / _behavior.Symptoms.Count()) * 100;
            if (percentageMatch >= MIN_PERCENTAGE)
            {
                return BaseResponse.CreateSuccess($"{_behavior.Name} ; {percentageMatch}");
            }
            return BaseResponse.CreateFailure("Does Not Match");
        }
        private bool AllConditionsMet()
        {
            return _behavior.Symptoms?.Any() ?? false;
        }
    }

}

