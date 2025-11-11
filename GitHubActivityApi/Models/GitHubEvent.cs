namespace GitHubActivityApi.Models;

// This class represents the minimal data we need from a GitHub Event API response
public class GitHubEvent
{
    // The type of activity (e.g., PushEvent, CreateEvent, PullRequestEvent)
    public string Type { get; set; } = string.Empty;

    // The date and time the event occurred
    // DateTimeOffset is often safer for API dates than DateTime, as it handles time zone info.
    public DateTimeOffset CreatedAt { get; set; }

    // Payload is an object that contains event-specific details. 
    // We can handle this later or keep it as an object for now.
    public object? Payload { get; set; }

    // Actor object contains the user who triggered the event
    public Actor? Actor { get; set; }

    // Repository object contains information about the repo involved
    public Repository? Repo { get; set; }
}

// A simplified model for the Actor (the user who performed the action)
public class Actor
{
    public string Login { get; set; } = string.Empty;
}

// A simplified model for the Repository
public class Repository
{
    public string Name { get; set; } = string.Empty;
}