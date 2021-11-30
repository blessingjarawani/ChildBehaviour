using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.DAL.Entities
{
    public class BehaviourSymptoms : BaseEntity
    {
        [ForeignKey("Behaviour")]
        public int BehaviourId { get; set; }

        [ForeignKey("Symptom")]
        public int SymptomId { get; set; }

        public virtual Behaviour Behaviour { get; set; }

        public virtual Symptom Symptom { get; set; }
    }
}
