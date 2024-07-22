using FakeDataAPI.Models;
using FakeDataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FakeDataAPI.Controllers
{
    [Route("api/[controller]")]
    //[Route("/")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IExternalAPIService _externalAPIService;

        public TopicController(IExternalAPIService externalAPIService) 
        {
            _externalAPIService = externalAPIService;
        }

        // GET: api/get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Topic> topics = new List<Topic>();
                HttpResponseMessage response = await _externalAPIService.GetTopicsAll();

                if (response.IsSuccessStatusCode)
                {
                    //json returned here, need to put parse into appropriate class object collection
                    string contentString= await response.Content.ReadAsStringAsync();
                    topics = JsonConvert.DeserializeObject<List<Topic>>(contentString) ??
                                throw new InvalidOperationException();
                    return Ok(new { topics });
                }
                else
                {
                    return StatusCode(500, new { Message = "Internal Error in Topic controller Get api method." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/get/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                Topic topic = new Topic();
                HttpResponseMessage response = await _externalAPIService.GetTopicByTopicId(id);

                if (response.IsSuccessStatusCode)
                {
                    //json returned here, need to put parse into appropriate class object collection
                    string contentString = await response.Content.ReadAsStringAsync();
                    topic = JsonConvert.DeserializeObject<Topic>(contentString) ??
                                throw new InvalidOperationException();
                    return Ok(new { topic });
                }
                else
                {
                    return StatusCode(500, new { Message = "Internal Error in Topic controller Get/id api method." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/get-comments/5
        [HttpGet("get-comments/{topicid}")]
        public async Task<IActionResult> GetTopicComments(int topicid)
        {
            try
            {
                List<Comment> comments = new List<Comment>();
                HttpResponseMessage response = await _externalAPIService.GetCommentsForTopic(topicid);

                if (response.IsSuccessStatusCode)
                {
                    //json returned here, need to put parse into appropriate class object collection
                    string contentString = await response.Content.ReadAsStringAsync();
                    comments = JsonConvert.DeserializeObject<List<Comment>>(contentString) ??
                                throw new InvalidOperationException();
                    return Ok(new { comments });
                }
                else
                {
                    return StatusCode(500, new { Message = "Internal Error in Topic controller GetTopicComments/id api method." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/post
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/put/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/delete/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
