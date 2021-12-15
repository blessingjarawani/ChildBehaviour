using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChildBehaviour.UI
{
    public class ApiClient
    {

        private HttpClient httpClient;
        public HttpClient InitClient()
        {
            httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 0, 0, 20);
            var baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            httpClient.BaseAddress = new Uri(baseUrl);
            return httpClient;
        }

    }
}
