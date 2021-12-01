using System.ComponentModel.DataAnnotations.Schema;

namespace ChildBehaviour.DAL.Entities
{
    public class ChildAssement : BaseEntity
    {

        [ForeignKey("Behaviour")]
        public int BehaviourId { get; set; }
      
        [ForeignKey("Pupil")]
        public int ChildId { get; set; }

        public virtual Behaviour Behaviour { get; set; }

        public virtual Pupil Pupil { get; set; }
    }
}
