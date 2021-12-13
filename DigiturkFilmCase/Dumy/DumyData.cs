using Bogus;
using DigiturkFilmCase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiturkFilmCase.Dumy_Data
{
    public static class DumyData
    {
        private static List<Film> _films;
        private static List<User> _users;

        public static List<Film> GetFilms(int number)
        {
            //Films Dumy data için Bogus kütüphanesi kullanıldı.
            if(_films==null)
            {
                _films = new Faker<Film>()
                .RuleFor(f => f.Id, x => x.IndexFaker + 1)
                .RuleFor(f => f.Name, x => x.Name.JobTitle())
                .RuleFor(f => f.Category, x => x.Name.JobArea())
                .RuleFor(f => f.Detail, x => x.Lorem.Sentence(3, 5))
                .RuleFor(f => f.ContentPlay, x => x.Name.JobTitle())
                .RuleFor(f => f.AudienceScorePercent, x => x.Random.Long(1, 100))
                .Generate(number);
            }
            return _films;
        }
        public static List<User> GetUsers(int number)
        {
           if (_users == null)
            {
                _users = new Faker<User>()
                .RuleFor(f => f.Id, x => x.IndexFaker + 1)
                .RuleFor(f => f.FullName, x => x.Name.FullName())
                .RuleFor(f => f.UserKey, x => x.Random.AlphaNumeric(5))
                .RuleFor(f => f.Role, x => x.Random.Long(1,2))
                .Generate(number);
            }
            return _users;
        }
    }
}
