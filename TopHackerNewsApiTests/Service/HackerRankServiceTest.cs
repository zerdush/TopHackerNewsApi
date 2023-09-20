using Moq;
using TopHackerNewsApi.Domain;
using TopHackerNewsApi.Service;

namespace TopHackerNewsApiTests.Service;

public class HackerRankServiceTest
{
    [Test]
    public void GivenThereAreEnoughTopStoriesWhenGetTopStoriesThenReturnTopStories()
    {
        var clientMock = new Mock<IHackerRankClient>();
        var expectedStory1 = new Story("title", "uri", "postedBy", "time", 3, 1);
        var expectedStory2 = new Story("title", "uri", "postedBy", "time", 2, 1);
        var expectedStory3 = new Story("title", "uri", "postedBy", "time", 1, 1);
        clientMock.Setup(c => c.GetTopStories()).Returns(new List<int>
        {
            1, 2, 3, 4, 5
        });
        clientMock.Setup(c => c.GetStory(1)).Returns(expectedStory1);
        clientMock.Setup(c => c.GetStory(2)).Returns(expectedStory2);
        clientMock.Setup(c => c.GetStory(3)).Returns(expectedStory3);
        var service = new HackerRankService(clientMock.Object);
        
        var result = service.GetTopStories(3);
        
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.Multiple(() =>
        {
            Assert.That(result[0], Is.EqualTo(expectedStory1));
            Assert.That(result[1], Is.EqualTo(expectedStory2));
            Assert.That(result[2], Is.EqualTo(expectedStory3));
        });
        clientMock.Verify(c => c.GetStory(4), Times.Never);
        clientMock.Verify(c => c.GetStory(5), Times.Never);
    }

    [Test]
    public void GivenThereAreNotEnoughTopStoriesWhenGetTopStoriesThenReturnWhatIsThere()
    {
        var clientMock = new Mock<IHackerRankClient>();
        var expectedStory1 = new Story("title", "uri", "postedBy", "time", 3, 1);
        var expectedStory2 = new Story("title", "uri", "postedBy", "time", 2, 1);
        clientMock.Setup(c => c.GetTopStories()).Returns(new List<int>
        {
            1, 2
        });
        clientMock.Setup(c => c.GetStory(1)).Returns(expectedStory1);
        clientMock.Setup(c => c.GetStory(2)).Returns(expectedStory2);
        var service = new HackerRankService(clientMock.Object);
        
        var result = service.GetTopStories(3);
        
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(result[0], Is.EqualTo(expectedStory1));
            Assert.That(result[1], Is.EqualTo(expectedStory2));
        });
    }
}