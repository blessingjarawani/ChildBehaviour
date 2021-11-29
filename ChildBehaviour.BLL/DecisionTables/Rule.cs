using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
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
        public Rule(BehaviourDto behaviour)
        {
            _behavior = behaviour;
        }
        public IResponse Execute(IEnumerable<Symptom> symptoms)
        {
            if (AllConditionsMet())
            { }
               // _action.Invoke();
        }
        private bool AllConditionsMet()
        {
            return _behavior.Symptoms?.Any() ?? false;
        }
    }

}

