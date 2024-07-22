using FakeDataAPI.Models;

namespace FakeDataAPI.Services.Interfaces
{
    public interface IExternalAPIService
    {
        Task<HttpResponseMessage> GetTopicsAll();
        Task<HttpResponseMessage> GetTopicByTopicId(int TopicId);
        Task<HttpResponseMessage> GetCommentsForTopic(int TopicId);
        Task<HttpResponseMessage> GetUsers();
    }
}
