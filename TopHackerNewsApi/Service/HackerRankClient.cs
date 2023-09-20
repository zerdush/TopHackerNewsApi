using Newtonsoft.Json;
using TopHackerNewsApi.Domain;

namespace TopHackerNewsApi.Service;

public class HackerRankClient: IHackerRankClient
{
    private const string BestStoriesEndPoint = "https://hacker-news.firebaseio.com/v0/beststories.json";
    private const string StoryDetailEndPoint = "https://hacker-news.firebaseio.com/v0/item/{0}.json";
    private readonly HttpClient _httpClient;

    public HackerRankClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public IList<int> GetTopStories()
    {
        var response = _httpClient.GetAsync(BestStoriesEndPoint).Result;
        var integers = JsonConvert.DeserializeObject<List<int>>(response.Content.ReadAsStringAsync().Result) 
                       ?? new List<int>();
        return integers;
    }

    public Story GetStory(int id)
    {
        var response = _httpClient.GetAsync(string.Format(StoryDetailEndPoint, id)).Result;
        var hackerStory = JsonConvert.DeserializeObject<HackerStory>(response.Content.ReadAsStringAsync().Result);
        var story = new Story(
            Title: hackerStory.Title, 
            Uri: hackerStory.Url, 
            PostedBy: hackerStory.By,
            Time: DateTimeOffset.FromUnixTimeSeconds(hackerStory.Time).ToString("yyyy-MM-ddTHH:mm:sszzz"),
            Score: hackerStory.Score, 
            CommentCount: hackerStory.Descendants);
        return story;
    }
    
    public record HackerStory(
        string By, 
        int Descendants, 
        int Id, 
        List<int> Kids, 
        int Score, 
        long Time, 
        string Title, 
        string Type, 
        string Url);
}