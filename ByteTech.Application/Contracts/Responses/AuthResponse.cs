namespace ByteTech.Application.Contracts.Responses;

public record AuthResponse(string AccessToken, UserResponse User);

