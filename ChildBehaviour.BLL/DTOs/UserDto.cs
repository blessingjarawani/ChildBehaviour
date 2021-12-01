using ChildBehaviour.BLL.Abstracts.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public string Password { get; set; }
        public string UserName { get; set; }
        public UserRoles UserRole { get; set; }
        public string FullName => $"{LastName} {FirstName}";
    }
}

