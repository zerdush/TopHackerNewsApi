using TopHackerNewsApi.Domain;

namespace TopHackerNewsApi.Service;

public interface IHackerRankService
{
    IList<Story> GetTopStories(int topN);
}