using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        protected T Get<T>(string url) where T : new() => GetAsync<T>(url).Result;
        protected async Task<T>GetAsync<T>(string url, CancellationToken Cancel = default) where T: new()
        {
            var respronse = await _Client.GetAsync(url, Cancel);
            if (respronse.IsSuccessStatusCode)
            {
                return await respronse.Content.ReadAsAsync<T>(Cancel);
            }
            return new T();
        }
        protected HttpResponseMessage Post<T>(string url, T item) => PostAsync(url, item).Result;
        protected async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            var response = await _Client.PostAsJsonAsync(url, item, Cancel);
            return response.EnsureSuccessStatusCode();
        }
        protected HttpResponseMessage Put<T>(string url, T item) => PutAsync(url, item).Result;
        protected async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            var response = await _Client.PutAsJsonAsync(url, item, Cancel);
            return response.EnsureSuccessStatusCode();
        }


        protected HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
        protected async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken Cancel = default)
        {
            return await _Client.DeleteAsync(url,Cancel);
        }
    }
}
