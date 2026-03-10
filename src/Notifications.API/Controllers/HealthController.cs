using Microsoft.AspNetCore.Mvc;

namespace Notifications.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    /// <summary>Returns service health status</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get() =>
        Ok(new
        {
            Status = "Healthy",
            Service = "NotificationsAPI",
            Description = "Consumes UserCreatedEvent and PaymentProcessedEvent, simulates email notifications via console logs",
            Timestamp = DateTime.UtcNow
        });
}
