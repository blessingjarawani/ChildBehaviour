using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.DAL.Entities
{
    public class Behaviour : BaseEntity
    {
        [ForeignKey("Symptom")]
        public int SymptomId { get; set; }
        public List<Symptom> Symptoms { get; set; }
    }
}
