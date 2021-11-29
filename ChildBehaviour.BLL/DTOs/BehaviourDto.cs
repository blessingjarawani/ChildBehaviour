using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.DTOs
{
    public class BehaviourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SymptomDto> Symptoms { get; set; }
    }
}
