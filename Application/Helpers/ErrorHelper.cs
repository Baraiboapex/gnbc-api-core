using System.Collections;
using System.Data;
using System.Threading.Tasks;
using System;

namespace Application.Helpers
{
    public interface IErrorHelper{
        public NewError CreateError(int code, string message);
    }
    
    public class ErrorHelper : IErrorHelper
    {
        public NewError CreateError(int code, string message)
        {
            var newError = new NewError();

            newError.AddValue(code, message);
            
            throw newError;
        }
    }
}