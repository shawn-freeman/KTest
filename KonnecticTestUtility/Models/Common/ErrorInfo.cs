using System;
using System.Collections.Generic;
using System.Text;

namespace KonnecticTestCoreUtility.Models.Common
{
    public class ErrorInfo
    {
        public bool IsError { get; set; }
        public double Opacity { get; set; }
        public string Message { get; set; }

        public ErrorInfo()
        {
            IsError = false;
            Opacity = 0.0;
            Message = "";
        }
    }
}
