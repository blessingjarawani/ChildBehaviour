using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Abstracts.Response
{
    public interface IResponse
    {
        string Message { get; }
        bool Success { get; }
    }
}
