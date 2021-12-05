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
    public class BehaviourRepository : IBehaviourRespository
    {
        private readonly ChildBehaviourContext _context;

        public BehaviourRepository(ChildBehaviourContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BehaviourDto>> Get(int? id = null)
        {
            var query = _context.Behaviour.Where(t => t.IsActive).AsQueryable();
            if (id.HasValue && id.Value > 0)
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

        public async Task<int> Add(BehaviourDto behaviourDto)
        {
            var entity = new Behaviour
            {
                Name = behaviourDto.Name
            };
            await _context.Behaviour.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> Update(BehaviourDto behaviourDto)
        {
            var entity = _context.Behaviour.FirstOrDefault(x => x.Id == behaviourDto.Id);
            if (entity != null)
            {
                entity.Name = behaviourDto.Name;
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<IEnumerable<BehaviourDto>> GetBehaviourSymptoms(int id)
            => await _context.Behaviour.Include(t => t.BehaviourSymptoms)
               .ThenInclude(t => t.Symptom).Where(t => t.IsActive && t.Id == id).Select(t => new BehaviourDto
               {
                   Id = t.Id,
                   Name = t.Name,
                   Symptoms = t.BehaviourSymptoms.Select(x => new SymptomDto
                   {
                       Id = x.Symptom.Id,
                       Name = x.Symptom.Name
                   }).ToList()
               }).ToListAsync();

        public async Task<IEnumerable<BehaviourDto>> GetBehaviourRecommendations(int id)
             => await _context.Behaviour.Include(t => t.BehaviourRecommendations)
                 .Where(t => t.IsActive && t.Id == id).Select(t => new BehaviourDto
                 {
                     Id = t.Id,
                     Name = t.Name,
                     Recommendations = t.BehaviourRecommendations.Select(x => new RecommendationDto
                     {
                         Id = x.Id,
                         Name = x.Name
                     }).ToList()
                 }).ToListAsync();

        public async Task AddBehaviourSymptoms(BehaviourDto behaviour)
        {
            foreach (var symptom in behaviour.Symptoms)
            {
                var entity = new BehaviourSymptoms
                {
                    BehaviourId = behaviour.Id,
                    SymptomId = symptom.Id,
                    Name = "---"
                };
                await _context.BehaviourSymptoms.AddAsync(entity);
            }
            await _context.SaveChangesAsync();
        }
        public async Task AddBehaviourRecommendations(BehaviourDto behaviour)
        {
            foreach (var recommendations in behaviour.Recommendations)
            {
                var entity = new BehaviourRecommendations
                {
                    BehaviourId = behaviour.Id,
                    RecommendationId = recommendations.Id,
                    Name = "---"
                };
                await _context.BehaviourRecommendations.AddAsync(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveExcludedRangeRecommendations(int behaviourId)
        {
            var entitiesToRemove = _context.BehaviourRecommendations.Where(t => t.BehaviourId == behaviourId);
            if (entitiesToRemove?.Any() ?? false)
            {
                _context.BehaviourRecommendations.RemoveRange(entitiesToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveExcludedRangeSymptoms(int behaviourId)
        {
            var entitiesToRemove = _context.BehaviourSymptoms.Where(t => t.BehaviourId == behaviourId);
            if (entitiesToRemove?.Any() ?? false)
            {
                _context.BehaviourSymptoms.RemoveRange(entitiesToRemove);
                await _context.SaveChangesAsync();
            }
        }

    }
}
