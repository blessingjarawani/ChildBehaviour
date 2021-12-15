using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.DTOs
{
    public class LoginDto
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsValid =>
              !String.IsNullOrWhiteSpace(Password)
           && !String.IsNullOrWhiteSpace(UserName);
    }
}
