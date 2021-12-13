using DigiturkFilmCase.Common;
using DigiturkFilmCase.Common.Models;
using DigiturkFilmCase.Dumy_Data;
using DigiturkFilmCase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiturkFilmCase.Controllers
{
    [Route("[controller]")]
    public class HomeController:ControllerBase
    {
        const string cacheKey = "filmKey";
        private readonly IMemoryCache _memCache;
        private List<Film> _films = DumyData.GetFilms(10);
        private List<User> _users = DumyData.GetUsers(10);

        public HomeController(IMemoryCache memCache)
        {
            _memCache = memCache;
        }

        //Home/GetUserList
        //[Authorize(Roles ="1")]
        [Route("[action]")]
        public IActionResult GetUserList()
        {
            //Bu metot ile dummy data olarak oluşturulan tüm userlar listelenir.
            //Cache işlemi bu metoda uygulandı.
            if (_users == null)
                return NotFound();
            else
            {
                var users = _users.ToList();
                //Belirtilen key ile cache'de kontrolünü yapılıyor.
                if (!_memCache.TryGetValue(cacheKey,out _users))
                {
                    //Cache ayarlamaları yapılır.Cache süresi,önem derecesi belirlenir.
                    var cacheExpOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                        Priority = CacheItemPriority.Normal
                    };
                    //cache özelliklerine göre userlar in-memory olarak cache leme işlemi yapılır.
                    _memCache.Set(cacheKey, _users, cacheExpOptions);
                }
                return Ok(users);
            }
        }

        //Home/GetFilmList
        [Route("[action]")]
        public IActionResult GetFilmList()
        {
            //Bu metot ile dummy data olarak oluşturulan tüm filmler listelenir.
            if (_films == null)
                return NotFound();
            else
                return Ok(_films);
        }
        //Home/GetCategoryList
        [Route("[action]")]
        public IActionResult GetCategoryList()
        {
            //Tüm filmlerin Kategorilerini listelemek için yazıldı.
            var categories = _films.Select(x => x.Category).ToList();
            if (categories == null)
                return NotFound();
            else
            {
               return Ok(categories);
            }
        }
        //Home/GetFilmDetailByFilmId/5
        [Route("[action]/{id}")]
        public IActionResult GetFilmDetailByFilmId(int id)
        {
            //Seçilen herhangi bir filmin detaylarını listelemek için yazıldı.
            
            if (id <= 0)
            {
                throw new InvalidFilmException("Invalid film Id");
            }

            var detail = _films.Where(x => x.Id == id).Select(x => x.Detail).FirstOrDefault();

            if (detail == null)
            {
                return NotFound();
            }
            return Ok(detail);
        }
        //Home/GetFilmContentPlayByFilmId/5
        [Route("[action]/{id}")]
        public IActionResult GetFilmContentPlayByFilmId(int id)
        {
            //Seçilen herhangi bir filmin içerik play bilgisini vermek için yazıldı.
            if (id <= 0)
            {
                throw new InvalidFilmException("Invalid film Id");
            }
            var contentPlay = _films.Where(x => x.Id == id).Select(x => x.ContentPlay).FirstOrDefault();
            if (contentPlay == null)
                return NotFound();
            else
            {
               return Ok(contentPlay);
            }
        }
        [Route("[action]")]
        public IActionResult DeleteCache()
        {
            //Bu metotta verilen key'e göre bulunan veriyi cache den siliyoruz
            _memCache.Remove(cacheKey);
            return Ok();
        }
    }
}
