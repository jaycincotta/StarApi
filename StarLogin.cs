using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace StarApi
{
    public static class StarLogin
    {
        public static CookieContainer Login()
        {
            var client = new RestClient("https://star.ipo.vote/user/ajaxlogin/");
            client.CookieContainer = new CookieContainer();
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("email", "api");
            request.AddParameter("pass", "PCj4DsvyzADn9rY");
            request.AddParameter("authLength", "0");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return client.CookieContainer;
        }
    }
}
