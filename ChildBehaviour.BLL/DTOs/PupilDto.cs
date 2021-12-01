using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.DTOs
{
    public class PupilDto
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string FullName => $"{Surname} {FirstName}";
        public ParentDto Parent { get; set; }
    }
}
