using System;
using System.Collections.Generic;
using System.Text;

namespace ERSA.Mobile.AdminApi
{
    public struct Result
    {
        public Result(bool success)
        {
            Success = success;
            Message = null;
        }
        public Result(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public bool Success;
        public string Message;
    }
}
