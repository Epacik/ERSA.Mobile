using System;
using System.Collections.Generic;
using System.Text;

namespace ERSA.Mobile.Definitions
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public sealed class PreserveAttribute : System.Attribute
    {
        public bool AllMembers;
        public bool Conditional;
    }
}
