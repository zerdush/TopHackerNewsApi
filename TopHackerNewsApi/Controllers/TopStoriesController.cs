using Microsoft.AspNetCore.Mvc;
using TopHackerNewsApi.Domain;
using TopHackerNewsApi.Service;

namespace TopHackerNewsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TopStoriesController : ControllerBase
{
    private readonly IHackerRankService _hackerRankService;

    public TopStoriesController(IHackerRankService hackerRankService)
    {
        _hackerRankService = hackerRankService;
    }
    
    [HttpGet(Name = "GetTopStories")]
    public IEnumerable<Story> Get([FromQuery] int topN = 5)
    {
        return _hackerRankService.GetTopStories(topN);
    }
}