using ChildBehaviour.BLL.Abstracts.Repositories;
using ChildBehaviour.BLL.DTOs;
using ChildBehaviour.DAL.Context;
using ChildBehaviour.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.DAL.Repositories
{
    public class SymptomsRepository : ISymptomsRepository
    {
        private readonly ChildBehaviourContext _context;

        public SymptomsRepository(ChildBehaviourContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SymptomDto>> Get(int? id)
        {
            var query = _context.Symptom.Where(t => t.IsActive).AsQueryable();
            if (id.HasValue)
            {
                query = query.Where(t => t.Id == id);
            }
            return await query.Select(t => new SymptomDto
            {
                Id = t.Id,
                Name = t.Name,
            }).ToListAsync();
        }


        public async Task<int> Add(SymptomDto symptom)
        {
            var entity = new Symptom
            {
                Name = symptom.Name
            };
            await _context.Symptom.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<int> Update(SymptomDto symptom)
        {
            var entity = _context.Symptom.FirstOrDefault(x => x.Id == symptom.Id);
            if (entity != null)
            {
                entity.Name = symptom.Name;
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task RemoveExcludedRange(List<int> ids)
        {
            var entitiesToRemove = _context.Symptom.Where(t => !ids.Contains(t.Id));
            if (entitiesToRemove.Any())
            {
                _context.Symptom.RemoveRange(entitiesToRemove);
                await _context.SaveChangesAsync();
            }
        }

    }
}
