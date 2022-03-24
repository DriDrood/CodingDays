using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TempApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult> LoginTeam(LoginReq param)
    {
        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://coding-days.samuell.ovh/api/team/login");
        message.Content = new StringContent(JsonSerializer.Serialize(param), Encoding.UTF8, "application/json");

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.SendAsync(message);
            
            string responseBody = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, responseBody);
        }
    }
}
