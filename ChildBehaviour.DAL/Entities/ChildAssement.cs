using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
