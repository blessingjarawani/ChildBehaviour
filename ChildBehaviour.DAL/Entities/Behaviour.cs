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
        public List<BehaviourSymptoms> BehaviourSymptoms { get; set; }
        public List<BehaviourRecommendations> BehaviourRecommendations { get; set; }
        public List<ChildAssement> ChildAssements { get; set; }
    }
}
