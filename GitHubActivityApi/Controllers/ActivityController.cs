using Microsoft.AspNetCore.Mvc;
using GitHubActivityApi.Services;
using GitHubActivityApi.Models;

namespace GitHubActivityApi.Controllers;

[ApiController]
[Route("api/[controller]")] // Base route: /api/activity
public class ActivityController : ControllerBase
{
    // Take The Private Section
    private readonly IGitHubService _gitHubService;

    //  Use IGitHubService for Constructor Injection
    public ActivityController(IGitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    // Action Method For User 
    // Route will be: /api/activity/{username}
    [HttpGet("{username}")]
    [ProducesResponseType(typeof(List<GitHubEvent>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserActivity(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return BadRequest("Username cannot be empty.");
        }

        // Calling Service For Event
        var events = await _gitHubService.GetUserActivityAsync(username);

        if (events == null)
        {
            return NotFound($"GitHub user '{username}' not found or could not retrieve data.");
        }

        // 200 Status
        return Ok(events);
    }
}