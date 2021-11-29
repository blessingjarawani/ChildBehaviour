using ChildBehaviour.BLL.Abstracts.DecisionTable;
using ChildBehaviour.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.DecisionTables
{
    public class Symptom : ISymptom
    {
        public SymptomDto _Symptom { get; }
        public Symptom(SymptomDto symptom)
        {
            _Symptom = symptom;
        }
    }
}
