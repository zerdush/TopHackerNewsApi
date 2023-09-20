namespace TopHackerNewsApi.Domain;

public record Story(string Title, string Uri, string PostedBy, string Time, int Score, int CommentCount);