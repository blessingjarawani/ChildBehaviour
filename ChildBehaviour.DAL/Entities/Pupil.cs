using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.DAL.Entities
{
    public class Pupil : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Surname { get; set; }

        [Required]
        public DateTime DOB { get; set; }
        public List<ChildAssement> ChildAssements { get; set; }
        [ForeignKey("User")]
        public int ParentId { get; set; }
        public User User { get; set; }
    }
}
