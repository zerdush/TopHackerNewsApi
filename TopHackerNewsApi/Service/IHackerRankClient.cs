using TopHackerNewsApi.Domain;

namespace TopHackerNewsApi.Service;

public interface IHackerRankClient
{
    IList<int> GetTopStories();
    Story GetStory(int id);
}