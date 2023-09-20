using Microsoft.Extensions.Caching.Memory;
using Moq;
using TopHackerNewsApi.Domain;
using TopHackerNewsApi.Service;

namespace TopHackerNewsApiTests.Service;

public class CachedHackerRankClientTest
{
    private MemoryCache _cache;

    [SetUp]
    public void Setup()
    {
        _cache = new MemoryCache(new MemoryCacheOptions());
    }
    
    [TearDown]
    public void TearDown()
    {
        _cache.Dispose();
    }
    
    [Test]
    public void GivenTopStoriesAreNotInCacheWhenGetTopStoriesThenReturnTopStoriesFromHackerRankClient()
    {
        var hackerRankClientMock = new Mock<IHackerRankClient>();
        List<int> expectedTopStories = new(){1, 2, 3};
        hackerRankClientMock.Setup(c => c.GetTopStories()).Returns(expectedTopStories);
        
        var client = new CachedHackerRankClient(hackerRankClientMock.Object, _cache);
        
        var result = client.GetTopStories();
        
        Assert.That(result, Is.EqualTo(expectedTopStories));
    }
    
    [Test]
    public void GivenTopStoriesAreNotInCacheWhenGetTopStoriesThenPopulateCacheFromHackerRankClient()
    {
        var hackerRankClientMock = new Mock<IHackerRankClient>();
        List<int> expectedTopStories = new(){1, 2, 3};
        hackerRankClientMock.Setup(c => c.GetTopStories()).Returns(expectedTopStories);
        
        var client = new CachedHackerRankClient(hackerRankClientMock.Object, _cache);
        client.GetTopStories();
        
        var result = _cache.Get<IList<int>>("topStories");
        
        Assert.That(result, Is.EqualTo(expectedTopStories));
    }

    [Test]
    public void GivenTopStoriesAreInCacheWhenGetTopStoriesThenReturnItFromCache()
    {
        var hackerRankClientMock = new Mock<IHackerRankClient>();
        hackerRankClientMock.Setup(c => c.GetTopStories()).Returns(new List<int>());
        
        List<int> expectedTopStories = new(){1, 2, 3};
        _cache.Set("topStories", expectedTopStories );
        
        var client = new CachedHackerRankClient(hackerRankClientMock.Object, _cache);
        
        var result = client.GetTopStories();
        
        Assert.That(result, Is.EqualTo(expectedTopStories));
    }
    
    [Test]
    public void GivenStoryIsNotInCacheWhenGetStoryThenReturnStoryFromHackerRankClient()
    {
        var hackerRankClientMock = new Mock<IHackerRankClient>();
        var expectedStory = new Story("title", "uri", "postedBy", "time", 1, 1);
        hackerRankClientMock.Setup(c => c.GetStory(1)).Returns(expectedStory);
        
        var client = new CachedHackerRankClient(hackerRankClientMock.Object, _cache);
        
        var result = client.GetStory(1);
        
        Assert.That(result, Is.EqualTo(expectedStory));
    }
    
    [Test]
    public void GivenStoryIsNotInCacheWhenGetStoryThenPopulateCacheFromHackerRankClient()
    {
        var hackerRankClientMock = new Mock<IHackerRankClient>();
        var expectedStory = new Story("title", "uri", "postedBy", "time", 1, 1);
        hackerRankClientMock.Setup(c => c.GetStory(1)).Returns(expectedStory);
        
        var client = new CachedHackerRankClient(hackerRankClientMock.Object, _cache);
        client.GetStory(1);
        
        var result = _cache.Get<Story>(1);
        
        Assert.That(result, Is.EqualTo(expectedStory));
    }
    
    [Test]
    public void GivenStoryIsInCacheWhenGetStoryThenReturnItFromCache()
    {
        var hackerRankClientMock = new Mock<IHackerRankClient>();
        var expectedStory = new Story("title", "uri", "postedBy", "time", 1, 1);
        _cache.Set(1, expectedStory);
        
        var client = new CachedHackerRankClient(hackerRankClientMock.Object, _cache);
        
        var result = client.GetStory(1);
        
        Assert.That(result, Is.EqualTo(expectedStory));
    }
    
}