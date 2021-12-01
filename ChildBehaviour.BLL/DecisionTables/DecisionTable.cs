using ChildBehaviour.BLL.Abstracts.DecisionTable;
using ChildBehaviour.BLL.Abstracts.Response;
using ChildBehaviour.BLL.DTOs;
using System;
using System.Collections.Generic;

namespace ChildBehaviour.BLL.DecisionTables
{
    public class DecisionTable : IDecisionTable
    {

        private List<Symptom> _conditions = new List<Symptom>();
        private List<Rule> _rules = new List<Rule>();
        public ISymptom AddQuestionareSymptoms(SymptomDto symptom)
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
        public List<IResponse> Execute()
        {
            var result = new List<IResponse>();
            _rules.ForEach(r =>
            {
                result.Add(r.Execute(_conditions));
            });
            return result;
        }
    }
}
