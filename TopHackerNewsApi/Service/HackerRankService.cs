using TopHackerNewsApi.Domain;

namespace TopHackerNewsApi.Service;

public class HackerRankService: IHackerRankService
{
    private readonly IHackerRankClient _hackerRankClient;

    public HackerRankService(IHackerRankClient hackerRankClient)
    {
        _hackerRankClient = hackerRankClient;
    }

    public IList<Story> GetTopStories(int topN)
    {
        return _hackerRankClient.GetTopStories().AsParallel().Select(storyId => _hackerRankClient
            .GetStory(storyId))
            .Take(topN)
            .ToList();
    }
}