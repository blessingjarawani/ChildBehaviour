using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.DTOs
{
    public class ChildAssessmentDto
    {
        public int PupilId { get; set; }
        public List<BehaviourDto> Behaviours { get; set; }
    }
}
