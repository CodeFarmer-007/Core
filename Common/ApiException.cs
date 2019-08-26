using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ApiException : Exception
    {
        public int ReturnCode = 1;
        public ApiException(string msg, int code = 1) : base(msg)
        {
            ReturnCode = code;
        }
    }
}
