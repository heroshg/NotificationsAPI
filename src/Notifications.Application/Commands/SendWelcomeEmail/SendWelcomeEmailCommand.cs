using MediatR;

namespace Notifications.Application.Commands.SendWelcomeEmail;

public record SendWelcomeEmailCommand(string Email, string Name) : IRequest;
