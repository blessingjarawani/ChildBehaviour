using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Response
{
    public interface IBaseResponse
    {
        string Message { get; }
        bool Success { get; }
    }
}
