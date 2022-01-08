using ERSA.Mobile.Shared;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ERSA.Mobile.AdminApi
{
    public class Client
    {
        /// <summary>
        /// Rest Api Client used for connecting with API
        /// </summary>
        private readonly RestClient client;

        /// <summary>
        /// Create an Admin API Client using a token stored in preferences
        /// </summary>
        public Client() : this(Preferences.Get(Shared.Constants.Preferences.ApiToken, null)) { }

        /// <summary>
        /// Create an Admin API Client using passed token
        /// </summary>
        /// <param name="token">Token used for connection with an API</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Client(string token)
        {
            if (token is null)
                throw new ArgumentNullException($"{nameof(token)} cannot be null");

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException($"{nameof(token)} cannot be empty");

            client = new RestClient("https://admin-lnk.epat.xyz");
            client.AddDefaultHeader("Authorization", $"Bearer {token}");
            client.ThrowOnAnyError = true;
            client.UseNewtonsoftJson();
        }

        /// <summary>
        /// ExecuteAsync in RestClient doesn't throw exceptions, so I'm doing it manually
        /// </summary>
        /// <param name="request"></param>
        /// <param name="httpMethod"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="System.Net.Http.HttpRequestException"></exception>
        private async Task<IRestResponse> ExecuteAsync(IRestRequest request, Method httpMethod, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await client.ExecuteAsync(request, httpMethod, cancellationToken);

            if (response.StatusCode == 0)
            {
                throw new System.Net.Http.HttpRequestException(response.ErrorMessage, response.ErrorException);
            }

            return response;
        }

        /// <summary>
        /// Test if server can be reached
        /// </summary>
        /// <returns>Whether the connection was successful</returns>
        public async Task<bool> TestConnectionAsync() =>
            await Exceptions.LogAndThrowAsync(async () =>
        {
            var request = new RestRequest(Shared.Constants.ApiEndpoints.TestConnection, Method.GET);
            request.RequestFormat = DataFormat.None;
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "text/plain");
            request.AddHeader("Accept", "*/*");
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            request.XmlSerializer = null;
            request.JsonSerializer = null;

            var response = await ExecuteAsync(request, Method.GET).ConfigureAwait(false);

            if (response is null) return false;

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }).ConfigureAwait(false);

        /// <summary>
        /// Add a new link to server
        /// </summary>
        /// <param name="linkToAdd">informations about link to add</param>
        /// <returns>Whether adding was successful</returns>
        public async Task<Result> AddLinkAsync(LinkToAdd linkToAdd) =>
            await Exceptions.LogAndThrowAsync(async () =>
            {
                var request = new RestRequest("/api/v1/add_link", DataFormat.Json);
                request.AddJsonBody(linkToAdd);
                var response = await ExecuteAsync(request, Method.PUT).ConfigureAwait(false);
                return new Result(response.StatusCode == System.Net.HttpStatusCode.Created, response.Content);
            }).ConfigureAwait(false);

        public async Task<LinkWithOpengraph> GetLinkDataAsync(int id) =>
            await GetLinkDataAsync(id.ToString()).ConfigureAwait(false);

        /// <summary>
        /// Get Link data with OpenGraph Tags
        /// </summary>
        /// <param name="idOrPath">ID of tag or path to a link</param>
        /// <returns></returns>
        public async Task<LinkWithOpengraph> GetLinkDataAsync(string idOrPath) =>
            await Exceptions.LogAndThrowAsync(async () =>
            {
                var request = new RestRequest($"/api/v1/get_link/{idOrPath}", DataFormat.Json);
                //request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();

                var response = await ExecuteAsync(request, Method.GET).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<LinkWithOpengraph>(response.Content);
            }).ConfigureAwait(false);

        /// <summary>
        /// Using a link ID update it's Path, Target and/or whether a link is hidden 
        /// </summary>
        /// <param name="linkToAdd">link to update</param>
        /// <returns>Result of updating</returns>
        public async Task<Result> UpdateLinkAsync(Link linkToAdd) =>
            await Exceptions.LogAndThrowAsync(async () =>
            {
                var request = new RestRequest("/api/v1/update_link", DataFormat.Json);
                request.AddJsonBody(linkToAdd);
                var response = await ExecuteAsync(request, Method.PATCH).ConfigureAwait(false);
                return new Result(response.StatusCode == System.Net.HttpStatusCode.OK, response.Content);
            }).ConfigureAwait(false);

        /// <summary>
        /// Remove a link using an ID
        /// </summary>
        /// <param name="id">ID of link</param>
        /// <returns>Result of removing</returns>
        public async Task<Result> RemoveLinkAsync(int id) => await RemoveLinkAsync(id.ToString());

        /// <summary>
        /// Remove a link using an ID or a path
        /// </summary>
        /// <param name="idOrPath">ID or path</param>
        /// <returns>Result of removing</returns>
        public async Task<Result> RemoveLinkAsync(string idOrPath) =>
            await Exceptions.LogAndThrowAsync(async () =>
           {
               var request = new RestRequest($"/api/v1/remove_link/{idOrPath}", DataFormat.Json);

               var response = await ExecuteAsync(request, Method.DELETE).ConfigureAwait(false);

               return new Result(response.StatusCode == System.Net.HttpStatusCode.OK, response.Content);
           }).ConfigureAwait(false);

        /// <summary>
        /// Add new OpenGraphTag to a link
        /// </summary>
        /// <param name="tagToAdd"></param>
        /// <returns></returns>
        public async Task<Result> AddOpenGraphTagAsync(OpengraphTag tagToAdd) =>
            await Exceptions.LogAndThrowAsync(async () =>
            {
                var request = new RestRequest("/api/v1/add_opengraph_tag", DataFormat.Json).AddJsonBody(tagToAdd);

                var response = await ExecuteAsync(request, Method.PUT).ConfigureAwait(false);

                return new Result(response.StatusCode == System.Net.HttpStatusCode.OK, response.Content);

            }).ConfigureAwait(false);

        /// <summary>
        /// Get an OpenGraph Tag data 
        /// </summary>
        /// <param name="tagId">ID of the tag</param>
        /// <returns>List of </returns>
        public async Task<OpengraphTag> GetOpenGraphTagAsync(int tagId) =>
            await Exceptions.LogAndThrowAsync(async () =>
        {
            var request = new RestRequest($"/api/v1/get_opengraph_tag/{tagId}", DataFormat.Json);
            //request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();

            var response = await ExecuteAsync(request, Method.GET).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<OpengraphTag>(response.Content);
        }).ConfigureAwait(false);

        /// <summary>
        /// Update the OpenGraph tag for a given link
        /// </summary>
        /// <param name="tagToUpdate"></param>
        /// <returns></returns>
        public async Task<Result> UpdateOpengraphTagAsync(OpengraphTag tagToUpdate) =>
            await Exceptions.LogAndThrowAsync(async () => {
                var request = new RestRequest("/api/v1/update_opengraph_tag").AddJsonBody(tagToUpdate);

                var response = await ExecuteAsync(request, Method.PATCH).ConfigureAwait(false);

                return new Result(response.StatusCode == System.Net.HttpStatusCode.OK, response.Content);
                }).ConfigureAwait(false);

        /// <summary>
        /// Remove an OpenGraph tag with a specified ID
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task<Result> RemoveOpenGraphTagAsync(int tagId) =>
            await Exceptions.LogAndThrowAsync<Result>(async () =>
            {
                var request = new RestRequest($"/api/v1/remove_opengraph_tag/{tagId}", DataFormat.Json);
                var response = await ExecuteAsync(request, Method.DELETE).ConfigureAwait(false);
                return new Result(response.StatusCode == System.Net.HttpStatusCode.OK, response.Content);
            }).ConfigureAwait(false);

        /// <summary>
        /// Get a list of link redirects on the server
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public async Task<Link[]> ListLinksAsync(string searchString = "") =>
            await Exceptions.LogAndThrowAsync(async () =>
            {
                var request = new RestRequest($"/api/v1/list_links/{searchString}", DataFormat.Json);
                //request.JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer();

                var response = await ExecuteAsync(request, Method.GET).ConfigureAwait(false);

                return JsonConvert.DeserializeObject<Link[]>(response.Content);
            }).ConfigureAwait(false);
    }
}
