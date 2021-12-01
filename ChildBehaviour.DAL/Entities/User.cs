using ChildBehaviour.BLL.Abstracts.Dictionaries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.DAL.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Surname { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }
        public UserRoles userRole { get; set; }
        public List<Pupil> Pupils { get; set; }
    }
}
