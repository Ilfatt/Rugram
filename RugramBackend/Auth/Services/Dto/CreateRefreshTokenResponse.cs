using Auth.Data.Models;

namespace Auth.Services.Dto;

public record CreateRefreshTokenResponse(string UnhashedTokenValue, RefreshToken RefreshToken);