using System;
using System.Collections.Generic;
using System.Text;

namespace Aguila.Core.Exceptions
{
   public class AguilaException : Exception
    {
        private int _status;
        public AguilaException()
        {
                
        }
        // Default badrequest
        public AguilaException(string message) : base(message)
        {
            _status = 400;
        }

        // custom errors
        public AguilaException(string message, int status) : base(message)
        {
            _status = status;
        }

        public int status {
            get { return _status; }
        }


    }
}
