using FakeDataAPI.Models;
using FakeDataAPI.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FakeDataAPI.Services
{
    public class ExternalAPIService : IExternalAPIService
    {
        private IHttpClientFactory _httpClientFactory;
        private IConfiguration _configuration;

        public ExternalAPIService(IHttpClientFactory httpClientFactory,
                                  IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> GetTopicsAll()
        {
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync(_configuration.GetSection("FakeDataApiDomain").Value + "/posts");
            return response;
        }

        public async Task<HttpResponseMessage> GetTopicByTopicId(int TopicId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync(_configuration.GetSection("FakeDataApiDomain").Value + "/posts/" + TopicId.ToString());
            return response;
        }

        public async Task<HttpResponseMessage> GetCommentsForTopic(int TopicId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync(_configuration.GetSection("FakeDataApiDomain").Value + "/posts/" + TopicId.ToString() + "/comments");
            return response;
        }

        public async Task<HttpResponseMessage> GetUsers()
        {
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync(_configuration.GetSection("FakeDataApiDomain").Value + "/users");
            return response;
        }
    }
}
