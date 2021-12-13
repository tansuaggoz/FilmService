using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiturkFilmCase.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Detail { get; set; }
        public string ContentPlay { get; set; }
        public long AudienceScorePercent { get; set; }
    }
}
