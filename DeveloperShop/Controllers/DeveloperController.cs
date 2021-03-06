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
            var calculatedList = new List<GitHubUser>();

            try
            {
                var organizationName = ConfigurationManager.AppSettings.Get("usernameOrganization");
                var urlApiMembers = string.Format("https://api.github.com/orgs/{0}/members", organizationName);

                var listGitHubUsers = JsonConvert.DeserializeObject<IList<GitHubUser>>(GetJsonString(urlApiMembers));

                foreach (var user in listGitHubUsers)
                {
                    GitHubUser gitHubUser = JsonConvert.DeserializeObject<GitHubUser>(GetJsonString(user.url));
                    user.avatar_url = gitHubUser.avatar_url;
                    user.price = EstimatedPrice(gitHubUser.public_repos, gitHubUser.followers);
                    user.public_repos = gitHubUser.public_repos;
                    user.followers = gitHubUser.followers;
                    user.email = gitHubUser.email;
                    user.name = gitHubUser.name;
                    user.id = gitHubUser.id;
                    user.login = gitHubUser.login;

                    calculatedList.Add(user)
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return calculatedList.AsEnumerable();
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
