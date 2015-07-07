using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using DeveloperShop.Models;

namespace DeveloperShop.Controllers
{
    public class DeveloperController : ApiController
    {
        public IEnumerable<GitHubUser> Get()
        {
            return this.GetGitHubUsers();
        }

        // GET: api/Developer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Developer
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Developer/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Developer/5
        public void Delete(int id)
        {
        }

        private IEnumerable<GitHubUser> GetGitHubUsers()
        {
            IEnumerable<GitHubUser> listGitHubUsers;

            //Teste
            var teste = new List<GitHubUser>();

            for (var i = 0; i <= 5; i++)
            {
                var gitHubUser = new GitHubUser
                {
                    avatar_url = "https://avatars0.githubusercontent.com/u/2496520?v=3&s=460",
                    email = "rafsouzap@icloud.com",
                    id = 10101010,
                    login = "rafsouzap",
                    name = "Rafael de Paula",
                    followers = 10,
                    public_repos = 10
                };
                gitHubUser.price = EstimatedPrice(gitHubUser.public_repos, gitHubUser.followers);

                teste.Add(gitHubUser);
            }

            listGitHubUsers = teste.AsEnumerable();

            //try
            //{
            //    var organizationName = ConfigurationManager.AppSettings.Get("usernameOrganization");
            //    var urlApiMembers = string.Format("https://api.github.com/orgs/{0}/members", organizationName);

            //    listGitHubUsers = JsonConvert.DeserializeObject<IList<GitHubUser>>(GetJsonString(urlApiMembers));

            //    foreach (var user in listGitHubUsers)
            //    {
            //        GitHubUser gitHubUser = JsonConvert.DeserializeObject<GitHubUser>(GetJsonString(user.url));
            //        user.price = EstimatedPrice(gitHubUser.public_repos, gitHubUser.followers);
            //        user.public_repos = gitHubUser.public_repos;
            //        user.followers = gitHubUser.followers;
            //        user.email = gitHubUser.email;
            //        user.name = gitHubUser.name;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            return listGitHubUsers;
        }

        private string GetJsonString(string url)
        {
            string json;

            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.Proxy = null;
            request.UserAgent = Guid.NewGuid().ToString();

            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    json = reader.ReadToEnd();
                }
            }

            return json;
        }

        private double EstimatedPrice(int userRepos, int userFollowers)
        {
            const double costPerRepo = 4.8;
            const double costPerFollower = 2.5;

            return ((userRepos*costPerRepo) + (userFollowers*costPerFollower));
        }
    }
}