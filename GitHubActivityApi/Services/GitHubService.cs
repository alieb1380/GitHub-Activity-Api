using GitHubActivityApi.Models;
using System.Text.Json;

namespace GitHubActivityApi.Services;

// Define an interface for DI
public interface IGitHubService
{
    Task<List<GitHubEvent>?> GetUserActivityAsync(string username);
}

public class GitHubService : IGitHubService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GitHubService> _logger;

    // Injection From HttpClient 
    public GitHubService(HttpClient httpClient, ILogger<GitHubService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        // GitHub API  Required User-Agent
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubActivityApi-Csharp");
    }

    public async Task<List<GitHubEvent>?> GetUserActivityAsync(string username)
    {
        // For Calling URL
        string url = $"https://api.github.com/users/{username}/events/public";

        try
        {
            // Sending Get Request
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            // 200 Status
            response.EnsureSuccessStatusCode();

            // Reading the content Like String
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON To the list
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            return JsonSerializer.Deserialize<List<GitHubEvent>>(content, options);
        }
        catch (HttpRequestException ex)
        {
            // Handle the Errors Like 404
            _logger.LogError(ex, "Error fetching GitHub activity for user {Username}", username);
            return null; // Null or 404
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred during GitHub API call.");
            return null;
        }
    }
}