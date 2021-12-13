using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiturkFilmCase.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserKey { get; set; }
        public long Role { get; set; }
    }
}
