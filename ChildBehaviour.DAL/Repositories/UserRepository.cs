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
    public class UserRepository : IUserRepository
    {
        private readonly ChildBehaviourContext _context;

        public UserRepository(ChildBehaviourContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserExists(string userName) =>
            string.IsNullOrWhiteSpace((await _context.User.FirstOrDefaultAsync(t => t.UserName == userName)).UserName) ? false : true;

        public async Task<int> Add(UserDto user)
        {
            var entity = new User
            {
                UserName = user.UserName,
                Name = user.FirstName,
                Surname = user.LastName,
                Password = user.Password,
                UserRole = user.UserRole
            };
            await _context.User.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }


        public async Task<int> Update(UserDto user)
        {
            var entity = _context.User.FirstOrDefault(x => x.Id == user.Id || x.UserName == user.UserName);
            if (entity != null)
            {
                entity.UserName = user.UserName;
                entity.Name = user.FirstName;
                entity.Surname = user.LastName;
                entity.Password = user.Password;
                entity.UserRole = user.UserRole;
                return await _context.SaveChangesAsync();
            }
            return -1;

        }

        public async Task<IEnumerable<PupilDto>> GetParentPupils(int userId)
            => await _context.Pupil.Where(x => x.IsActive && x.ParentId == userId)
                     .Select(x => new PupilDto
                     {
                         DOB = x.DOB,
                         FirstName = x.Name,
                         Id = x.Id,
                         Surname = x.Surname
                     }).ToListAsync();

        public async Task<UserDto> Login(UserDto user)
         => await _context.User.Where(x => x.UserName == user.UserName && x.Password == user.Password)
            .Select(x => new UserDto
            {
                LastName = x.Surname,
                UserName = x.UserName,
                UserRole = x.UserRole,
                FirstName = x.Name,
                Id = x.Id
            }).FirstOrDefaultAsync();
    }
}
