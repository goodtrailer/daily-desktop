using System;
using System.Collections.Generic;
using System.Text;

namespace DailyDesktop.Core.Providers
{
    public class ProviderException : Exception
    {
        public ProviderException()
        {
        }

        public ProviderException(string msg)
            : base(msg)
        {
        }
    }
}
