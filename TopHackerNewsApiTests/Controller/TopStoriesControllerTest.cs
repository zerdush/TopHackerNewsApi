using Moq;
using TopHackerNewsApi.Controllers;
using TopHackerNewsApi.Domain;
using TopHackerNewsApi.Service;

namespace TopHackerNewsApiTests.Controller;

public class TopStoriesControllerTest
{
    [Test]
    public void GivenServiceReturnsTopStoriesWhenGetThenReturn200WithStories()
    {
        var serviceMock = new Mock<IHackerRankService>();
        var expectedStory1 = new Story("title", "uri", "postedBy", "time", 1, 1);
        serviceMock.Setup(s => s.GetTopStories(5)).Returns(new List<Story>
        {
            expectedStory1
        });
        var topStoriesController = new TopStoriesController(serviceMock.Object);
        
        var result = topStoriesController.Get(5);
        
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First(), Is.EqualTo(expectedStory1));
    }
}