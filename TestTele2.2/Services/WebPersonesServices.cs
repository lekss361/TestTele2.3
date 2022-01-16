using TestTele2._2.DatBase;
using TestTele2._2.Model;
using System.Text.Json;

namespace TestTele2._2.Services
{
    public class WebPersonesServices
    {
        private readonly string _url;
        private static readonly HttpClient _httpClient = new HttpClient();

        public WebPersonesServices(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException(nameof(url));
            }

            _url = url;
        }

        public async Task<IEnumerable<Persone>>  GetPersones()
        {
            var result = new List<Persone>();
            List<WebPersoneInput> webPersones = new List<WebPersoneInput>();
            WebPersoneAgeInput personeAge= null;


            using (var response = await _httpClient.GetAsync(_url))
            using (var stream =await response.Content.ReadAsStreamAsync())
            {
                webPersones =JsonSerializer.Deserialize<List<WebPersoneInput>>(stream);

            }

            foreach (var wp in webPersones)
            {
                var persone = new Persone
                {
                    Id = wp.id,
                    Name = wp.name,
                    Sex = wp.sex

                };

                using (var response = await _httpClient.GetAsync(_url+"/"+persone.Id))
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    personeAge = JsonSerializer.Deserialize<WebPersoneAgeInput>(stream);

                }
                persone.Age = personeAge.age;
                result.Add(persone);
            }
                return result;

        }
    }
}
