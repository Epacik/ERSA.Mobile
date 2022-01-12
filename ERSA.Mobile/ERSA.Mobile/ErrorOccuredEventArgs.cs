using System;
using System.Collections.Generic;
using System.Text;

namespace ERSA.Mobile
{
    public class ErrorOccuredEventArgs : EventArgs
    {
        public readonly string ErrorMessage;

        public ErrorOccuredEventArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
