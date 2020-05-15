using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly string _ServiceAddress;
        protected readonly HttpClient _Client;
        protected BaseClient(IConfiguration Configuration, string ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;
            _Client = new HttpClient
            {
                BaseAddress = new Uri(Configuration["WebApiURL"]),
                DefaultRequestHeaders =
                {
                    Accept =
                    {
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                    }
                }
            };

        }
    }
}
