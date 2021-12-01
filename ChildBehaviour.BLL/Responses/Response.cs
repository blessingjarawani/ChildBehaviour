using ChildBehaviour.BLL.Abstracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Responses
{
    public  class Response : IResponse
    {
        public string Message { get; }

        public bool Success { get; }

        private Response(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        public static Response CreateSuccess(string message)
        {
            return new Response(message, success: true);
        }

        public static Response CreateFailure(string message)
        {
            return new Response(message, success: false);
        }
    }
}
