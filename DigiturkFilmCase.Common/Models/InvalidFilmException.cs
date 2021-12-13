using System;
using System.Collections.Generic;
using System.Text;

namespace DigiturkFilmCase.Common.Models
{
    public class InvalidFilmException:Exception
    {
        public InvalidFilmException(string message):base(message)
        {

        }
    }
}
