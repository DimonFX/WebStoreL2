using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Interfaces.Api;

namespace WebStore.Clients.Values
{
    public class MyTestClient : BaseClient,IMyTestService
    {
        public MyTestClient(IConfiguration Configuration) : base(Configuration, "api/mytest")
        {

        }

        public HttpStatusCode Delete(int id)
        {
            var response = _Client.DeleteAsync($"{_ServiceAddress}/delete/{id}").Result;
            return response.StatusCode;
        }

        public IEnumerable<double> Get()
        {
            var response = _Client.GetAsync(_ServiceAddress).Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<IEnumerable<double>>().Result;

            return Enumerable.Empty<double>();
        }

        public double Get(int id)
        {
            var response = _Client.GetAsync($"{_ServiceAddress}/{id}").Result;

            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<double>().Result;

            return double.NaN;
        }

        public Uri Post(double value)
        {
            var response = _Client.PostAsJsonAsync($"{_ServiceAddress}/post", value).Result;
            return response.EnsureSuccessStatusCode().Headers.Location;
        }

        public HttpStatusCode Update(int id, double value)
        {
            var response = _Client.PostAsJsonAsync($"{_ServiceAddress}/put/{id}", value).Result;
            return response.EnsureSuccessStatusCode().StatusCode;
        }
    }
}
