using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.DAL.Entities
{
    public class BehaviourRecommendations : BaseEntity
    {
        [ForeignKey("Behaviour")]
        public int BehaviourId { get; set; }

        [ForeignKey("Recommendation")]
        public int RecommendationId { get; set; }
        public virtual Behaviour Behaviour { get; set; }
        public virtual Recommendation Recommendation { get; set; }
    }
}
