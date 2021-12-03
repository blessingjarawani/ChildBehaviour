using ChildBehaviour.BLL.Abstracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.BLL.Responses
{
    public class BaseResponse : IBaseResponse
    {
        public string Message { get; }

        public bool Success { get; }

        private BaseResponse(bool success, string message = null)
        {
            Message = message;
            Success = success;
        }


        public static BaseResponse CreateSuccess(string message = null)
        {

            return new BaseResponse(success: true, message);
        }

        public static BaseResponse CreateFailure(string message)
        {
            return new BaseResponse(success: false, message);
        }
    }
}
