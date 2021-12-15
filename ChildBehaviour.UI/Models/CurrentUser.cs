using ChildBehaviour.BLL.DTOs;
using ChildBehaviour.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.UI.Models
{
    public class CurrentUser
    {
        public User User { get; protected set; }
        public void Set(UserDto user)
        {
            User = new User
            {
                Name = user.FirstName,
                Surname = user.LastName,
                Id = user.Id,
                UserName = user.UserName,
                UserRole = user.UserRole
            };
        }
    }
}
