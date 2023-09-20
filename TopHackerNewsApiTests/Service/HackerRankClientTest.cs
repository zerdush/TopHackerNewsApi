using TopHackerNewsApi.Service;

namespace TopHackerNewsApiTests.Service;

public class HackerRankClientTest
{
    [Test]
    public void WhenGetTopStoriesThenReturnTopStories()
    {
        var client = new HackerRankClient(new HttpClient());
        
        var result = client.GetTopStories();
        
        Assert.That(result.Count(), Is.EqualTo(200));
    }
    
    [Test]
    public void WhenGetStoryThenReturnStory()
    {
        var client = new HackerRankClient(new HttpClient());
        var result = client.GetTopStories();
        
        var story = client.GetStory(result.First());
        
        Assert.That(story, Is.Not.Null);
    }
}