using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonnecticTestCoreUtility.Models.Common
{
    public class ReturnResult<T>
    {
        public bool Success { get; set; }
        
        public ErrorInfo Error { get; set; }

        public T Result { get; set; }

        public ReturnResult() { Success = true; }

        public ReturnResult(T result)
        {
            Result = result;
            Success = true;

            Error = new ErrorInfo()
            {
                IsError = false,
                Message = "",
                Opacity = 0.0
            };
        }

        public ReturnResult(string error)
        {
            Error = new ErrorInfo()
            {
                IsError = true,
                Message = error,
                Opacity = 1.0
            };
        }
    }
}
