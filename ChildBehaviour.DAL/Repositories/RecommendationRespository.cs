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
    public class RecommendationRespository : IRecommendationRespository
    {
        private readonly ChildBehaviourContext _context;

        public RecommendationRespository(ChildBehaviourContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecommendationDto>> Get(int? id)
        {
            var query = _context.Recommendation.Where(t => t.IsActive).AsQueryable();
            if (id.HasValue)
            {
                query = query.Where(t => t.Id == id);
            }
            return await query.Select(t => new RecommendationDto
            {
                Id = t.Id,
                Name = t.Name,
            }).ToListAsync();
        }


        public async Task<int> Add(RecommendationDto recommendation)
        {
            var entity = new Recommendation
            {
                Name = recommendation.Name
            };
            await _context.Recommendation.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<int> Update(RecommendationDto recommendation)
        {
            var entity = _context.Recommendation.FirstOrDefault(x => x.Id == recommendation.Id);
            if (entity != null)
            {
                entity.Name = recommendation.Name;
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task DeleteRange(IEnumerable<int> ids)
        {
            var entitiesToRemove = _context.Recommendation.Where(t => ids.Contains(t.Id));
            if (entitiesToRemove.Any())
            {
                _context.Recommendation.RemoveRange(entitiesToRemove);
                await _context.SaveChangesAsync();
            }
        }

    }
}
