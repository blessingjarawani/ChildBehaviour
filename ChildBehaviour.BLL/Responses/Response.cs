using ChildBehaviour.BLL.Abstracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Responses
{
    public class Response<T> : IBaseResponse, IResponse<T>
    {
        public T Data { get; }
        public string Message { get; }

        public bool Success { get; }
        private Response(T data)
        {
            Data = data;
            Success = true;
        }

        private Response(string message)
        {

            Message = message;
            Success = false;
        }
        public static Response<T> CreateSuccess(T data)
           => new Response<T>(data);


        public static Response<T> CreateFailure(string message)
            => new Response<T>(message);

    }
}
