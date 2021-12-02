using ERSA.Mobile.Shared;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ERSA.Mobile.AdminApi
{
    public class Client
    {
        public Client() : this(Preferences.Get(Constants.Preferences.ApiToken, null)) { }
        public Client(string token)
        {
            if (token is null)
                throw new ArgumentNullException($"{nameof(token)} cannot be null");

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException($"{nameof(token)} cannot be empty");

            client = new RestClient("https://admin-lnk.epat.xyz");
            client.AddDefaultHeader("Authorization", $"Bearer {token}");
            client.ThrowOnAnyError = true;
            client.UseNewtonsoftJson();
        }

        private readonly RestClient client;

        public async Task<bool> TestConnection() => await Exceptions.LogAndCatchAsync(async () =>
        {
            var request = new RestRequest(Constants.ApiEndpoints.TestConnection, Method.GET);
            request.RequestFormat = DataFormat.None;
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "text/plain");
            request.AddHeader("Accept", "*/*");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            request.XmlSerializer = null;
            request.JsonSerializer = null;

            var response = await client.ExecuteGetAsync(request).ConfigureAwait(false);

            if (response is null) return false;

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }).ConfigureAwait(false);

        internal async Task<IEnumerable<object>> GetOpenGraphTags(int v)
        {
            await Task.Yield();
            //throw new NotImplementedException();
            return new List<object>();
        }

        public async Task<Link[]> ListLinks(string searchString = "") =>
            await Exceptions.LogAndCatchAsync(async () =>
            {
                var request = new RestRequest($"/api/v1/list_links/{searchString}", DataFormat.Json);
                //request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();

                var response = await client.ExecuteGetAsync(request).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<Link[]>(response.Content);
            }).ConfigureAwait(false);

        public async Task<LinkWithOpengraph> GetLinkData(string idOrPath) =>
            await Exceptions.LogAndCatchAsync(async () =>
            {
                var request = new RestRequest($"/api/v1/get_link/{idOrPath}", DataFormat.Json);
                //request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();

                var response = await client.ExecuteGetAsync(request).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<LinkWithOpengraph>(response.Content);
            });
    }
}
