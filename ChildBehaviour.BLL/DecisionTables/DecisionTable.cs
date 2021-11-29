using ChildBehaviour.BLL.Abstracts.DecisionTable;
using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.DecisionTables
{
    public class DecisionTable
    {
        
        private List<Symptom> _conditions = new List<Symptom>();
        private List<Rule> _rules = new List<Rule>();
        public ISymptom NewCondition(SymptomDto symptom)
        {
            if (null == symptom)
                throw new ArgumentNullException("predicate");    
            var condition = new Symptom(symptom);
            _conditions.Add(condition);
            return condition;
        }
       
        public void AddRule(BehaviourDto behaviour)
        {
            _rules.Add(new Rule(behaviour));
        }
        public IResponse Execute()
        {
          _rules.ForEach(r => r.Execute(_conditions));
        }
    }
}
