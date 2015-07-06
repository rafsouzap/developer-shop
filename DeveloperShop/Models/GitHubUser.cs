namespace DeveloperShop.Models
{
    public class GitHubUser
    {
        public int id { get; set; }
        public string login { get; set; }
        public string avatar_url { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int public_repos { get; set; }
        public int followers { get; set; }
        public double price { get; set; }
    }
}