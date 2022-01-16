namespace TestTele2._3.Models
{
    public class WebPersone
    {
        public string id { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        
    }

    public class WebPersones
    {
        public List<WebPersone> data { get; set; }
    }
}
