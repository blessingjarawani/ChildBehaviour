using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.DTOs
{
    public class BehaviourRecommandationsDto
    {
        public int BehaviourId { get; set; }
        public List<RecommendationDto> Recommendations { get; set; }
    }
}

