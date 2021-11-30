using ChildBehaviour.BLL.Abstracts.Repositories;
using ChildBehaviour.BLL.DTOs;
using ChildBehaviour.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.DAL.Repositories
{
    public class BehaviourRepository : IBehaviourRespository
    {
        private readonly ChildBehaviourContext _context;

        public BehaviourRepository(ChildBehaviourContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BehaviourDto>> Get(int? id)
        {
            var query = _context.Behaviour.Include(t => t.BehaviourSymptoms)
                .ThenInclude(t=>t.Symptom).Where(t => t.IsActive).AsQueryable();
            if (id.HasValue)
            {
                query = query.Where(t => t.Id == id);
            }

            return await query.Select(t => new BehaviourDto
            {
                Id = t.Id,
                Name = t.Name,
                Symptoms = t.BehaviourSymptoms.Select(x => new SymptomDto
                {
                    Id = x.Symptom.Id,
                    Name = x.Symptom.Name
                }).ToList()
            }).ToListAsync();

        }

    }
}
