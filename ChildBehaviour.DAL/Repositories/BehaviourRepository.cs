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
            var query = _context.Behaviour.Include(t => t.BehaviourSymptoms).ThenInclude(t=>t.Symptom).AsQueryable();
            if (id.HasValue && id.Value > 0)
            {
                query = query.Where(t => t.Id == id);
            }

            return await query.Select(t => new BehaviourDto
            {
                Id = t.Id,
                Name = t.Name,
                IsActive = t.IsActive,
                Symptoms = t.BehaviourSymptoms.Select(x => new SymptomDto
                {
                    Id = x.Symptom.Id,
                    IsActive = x.IsActive
                }).ToList(),
            }).ToListAsync();

        }

        public async Task<int> Add(BehaviourDto behaviourDto)
        {
            var entity = new Behaviour
            {
                Name = behaviourDto.Name,
                IsActive = behaviourDto.IsActive
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
                entity.IsActive = behaviourDto.IsActive;
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<List<BehaviourDto>> GetBehaviourSymptoms(int id)
            => await _context.Behaviour.Include(t => t.BehaviourSymptoms)
               .ThenInclude(t => t.Symptom).Where(t => t.Id == id).Select(t => new BehaviourDto
               {
                   Id = t.Id,
                   Name = t.Name,
                   Symptoms = t.BehaviourSymptoms.Select(x => new SymptomDto
                   {
                       Id = x.Symptom.Id,
                       Name = x.Symptom.Name,
                       IsActive = x.IsActive
                   }).ToList()
               }).ToListAsync();

        public async Task<IEnumerable<SymptomDto>> GetExcludedBehaviourSymptoms(IEnumerable<int> symptomsId)
       => await _context.Symptom.Where(t => !symptomsId.Contains(t.Id))
       .Select(t => new SymptomDto
       {
           Id = t.Id,
           Name = t.Name,
           IsActive = false
       }).ToListAsync();

        public async Task<List<BehaviourDto>> GetBehaviourRecommendations(int id)
             => await _context.Behaviour.Include(t => t.BehaviourRecommendations).ThenInclude(t => t.Recommendation)
                 .Where(t => t.IsActive && t.Id == id).Select(t => new BehaviourDto
                 {
                     Id = t.Id,
                     Name = t.Name,
                     Recommendations = t.BehaviourRecommendations.Select(x => new RecommendationDto
                     {
                         Id = x.Recommendation.Id,
                         Name = x.Recommendation.Name,
                         IsActive = x.IsActive
                     }).ToList()
                 }).ToListAsync();

        public async Task AddBehaviourSymptoms(BehaviourDto behaviour)
        {
            foreach (var symptom in behaviour.Symptoms)
            {
                var item = await _context.BehaviourSymptoms.FirstOrDefaultAsync(t => t.SymptomId == symptom.Id && t.BehaviourId == behaviour.Id);
                if (item != null)
                {
                    item.IsActive = symptom.IsActive;
                }
                else
                {
                    var entity = new BehaviourSymptoms
                    {
                        BehaviourId = behaviour.Id,
                        SymptomId = symptom.Id,
                        Name = "---",
                        IsActive = symptom.IsActive
                    };
                    await _context.BehaviourSymptoms.AddAsync(entity);
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task AddBehaviourRecommendations(BehaviourDto behaviour)
        {
            foreach (var recommendations in behaviour.Recommendations)
            {
                var item = await _context.BehaviourRecommendations.FirstOrDefaultAsync(t => t.RecommendationId == recommendations.Id && t.BehaviourId == behaviour.Id);
                if (item != null)
                {
                    item.IsActive = recommendations.IsActive;
                }
                else
                {
                    var entity = new BehaviourRecommendations
                    {
                        BehaviourId = behaviour.Id,
                        RecommendationId = recommendations.Id,
                        Name = "---",
                        IsActive = recommendations.IsActive
                    };
                    await _context.BehaviourRecommendations.AddAsync(entity);
                }

            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecommendationDto>> GetExcludedBehaviourRecommendations(IEnumerable<int> recommendationdIds)
          => await _context.Recommendation.Where(t => !recommendationdIds.Contains(t.Id))
          .Select(t => new RecommendationDto
          {
              Id = t.Id,
              Name = t.Name,
              IsActive = false
          }).ToListAsync();


    }
}
