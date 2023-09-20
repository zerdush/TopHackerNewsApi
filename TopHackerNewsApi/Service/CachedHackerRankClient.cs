using Microsoft.Extensions.Caching.Memory;
using TopHackerNewsApi.Domain;

namespace TopHackerNewsApi.Service;

public class CachedHackerRankClient : IHackerRankClient
{
    private readonly IHackerRankClient _hackerRankClient;
    private readonly IMemoryCache _memoryCache;

    public CachedHackerRankClient(IHackerRankClient hackerRankClient, IMemoryCache memoryCache)
    {
        _hackerRankClient = hackerRankClient;
        _memoryCache = memoryCache;
    }

    public IList<int> GetTopStories()
    {
        if (_memoryCache.TryGetValue("topStories", out List<int>? topStories) && topStories != null)
        {
            return topStories;
        }

        var topStoriesFromSource = _hackerRankClient.GetTopStories();
        _memoryCache.Set("topStories", topStoriesFromSource, TimeSpan.FromMinutes(15));
        return topStoriesFromSource;
    }

    public Story GetStory(int id)
    {
        if (_memoryCache.TryGetValue(id, out Story? story))
        {
            return story;
        }
        
        var storyFromSource = _hackerRankClient.GetStory(id);
        _memoryCache.Set(id, storyFromSource, TimeSpan.FromHours(1));
        return storyFromSource;
    }
}