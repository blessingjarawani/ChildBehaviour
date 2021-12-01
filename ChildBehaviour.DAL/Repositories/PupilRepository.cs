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
    public class PupilRepository : IPupilRespository
    {
        private readonly ChildBehaviourContext _context;

        public PupilRepository(ChildBehaviourContext context)
        {
            _context = context;
        }

        public async Task<int> Add(PupilDto pupil)
        {
            var entity = new Pupil
            {
                Name = pupil.FirstName,
                ParentId = pupil.ParentId,
                Surname = pupil.Surname,
                DOB = pupil.DOB
            };
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> Update(PupilDto pupil)
        {
            var entity = _context.Pupil.FirstOrDefault(t => t.Id == pupil.Id);
            if (entity != null)
            {
                entity.Name = pupil.FirstName;
                entity.Surname = pupil.Surname;
                entity.DOB = pupil.DOB;
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        public async Task<int> AddAssessment(ChildAssessmentDto childAssessment)
        {
            foreach (var behaviour in childAssessment.Behaviours)
            {
                var entity = new ChildAssement
                {
                    BehaviourId = behaviour.Id,
                    ChildId = childAssessment.PupilId
                };
                await _context.AddAsync(entity);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChildAssessmentDto>> GetPupilAssesments(int? id)
        {
            var query = _context.ChildAssement.Include(t => t.Behaviour).ThenInclude(t => t.BehaviourRecommendations).Where(t => t.IsActive);
            if (id.HasValue)
            {
                query = query.Where(t => t.ChildId == id.Value);
            }
            return (await query.ToListAsync()).GroupBy(t => t.ChildId)
                                  .Select(x => new ChildAssessmentDto
                                  {
                                      PupilId = x.Key,
                                      Behaviours = x.Select(t => new BehaviourDto
                                      {
                                          Id = t.Behaviour.Id,
                                          Name = t.Behaviour.Name,
                                          Recommendations = t.Behaviour.BehaviourRecommendations.Select(z => new RecommendationDto
                                          {
                                              Id = z.Id,
                                              Name = z.Name
                                          }).ToList(),
                                      }).ToList(),
                                  });
        }


    }
}
