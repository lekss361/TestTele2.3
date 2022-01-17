using Microsoft.AspNetCore.Mvc;
using TestTele2._2.Data;
using TestTele2._2.DatBase;
using TestTele2._2.Model;
using TestTele2._2.Services;

namespace TestTele2._2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersoneController : ControllerBase
    {
        // Persone
        [HttpGet]
        public Task<IEnumerable<Persone>> GetAsync()
        {
            string urlPersone = "http://testlodtask20172.azurewebsites.net/task";
            var servicesPersone = new WebPersonesServices(urlPersone);
            var persone = servicesPersone.GetPersones();

            using (var context = new DbTestTele2Context())
            {
                foreach (var item in persone.Result)
                {
                    var Persone = item;
                    context.Persones.Add(Persone);

                }
                context.SaveChanges();
            }
            return persone;
        }

        // Persone/{PersoneId}
        [HttpGet("{PersoneId}")]
        public Persone GetPersoneById(string personeId)
        {
            using (var context = new DbTestTele2Context())
            {
                return context.Persones.Find(personeId);
            }
        }

        // Persone/PageNumber/{pageNumber}
        [HttpGet("PageNumber/{pageNumber}")]
        public List<WebPersoneOutPut> GetPersonesByPage(int pageNumber)
        {
            int pageSize = 5; // кол-во элементов на странице
            int count = 0; // сколько всего элементов
            List<WebPersoneOutPut> pagePersones = new List<WebPersoneOutPut>();

            using (var context = new DbTestTele2Context())
            {
                count = context.Persones.Count();
                if (count > pageNumber * pageSize)
                {
                    var query = context.Persones.OrderBy(c => c.Id).Skip(pageSize * pageNumber).Take(pageSize);
                    foreach (var item in query)
                    {
                        WebPersoneOutPut webPersoneOutPut = new WebPersoneOutPut();
                        webPersoneOutPut.Id = item.Id;
                        webPersoneOutPut.Name = item.Name;
                        webPersoneOutPut.Sex = item.Sex;
                        pagePersones.Add(webPersoneOutPut);
                    }
                }

                return pagePersones;
            }

        }

        // Persone/sex/{sex}
        [HttpGet("sex/{sex}")]
        public List<WebPersoneOutPut> GetPersonesBySex(string sex)
        {
            List<WebPersoneOutPut> sortpersones = new List<WebPersoneOutPut>();

            using (var context = new DbTestTele2Context())
            {
                var query = from p in context.Persones
                            where p.Sex == sex
                            select new { p.Id, p.Name, p.Sex };

                foreach (var item in query)
                {
                    WebPersoneOutPut webPersoneOutPut = new WebPersoneOutPut();
                    webPersoneOutPut.Id = item.Id;
                    webPersoneOutPut.Name = item.Name;
                    webPersoneOutPut.Sex = item.Sex;
                    sortpersones.Add(webPersoneOutPut);
                }

            }
            return sortpersones;
        }

        // Persone/SortAgeFromMinToMax/min/{min}/max/{max}
        [HttpGet("SortAgeFromMinToMax/min/{min}/max/{max}")]
        public List<Persone> GetPersonesSortAgeFromMinToMax(int min, int max)
        {
            using (var context = new DbTestTele2Context())
            {
                var query = from p in context.Persones
                            where p.Age > min & p.Age < max
                            select p;
               return query.ToList();
            }
        }
    }
}