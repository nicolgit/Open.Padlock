using System;
using System.Collections.Generic;
using System.Text;

namespace nicold.Padlock.Models
{
    public class CloudSignInException: Exception
    {
        public CloudSignInException(string message): base(message)
        {

        }
    }
}
