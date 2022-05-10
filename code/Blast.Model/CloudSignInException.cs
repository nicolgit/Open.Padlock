using System;
using System.Collections.Generic;
using System.Text;

namespace Blast.Models
{
    public class CloudSignInException: Exception
    {
        public CloudSignInException(string message): base(message)
        {

        }
    }
}
