using BusinessManager.Domain.Entities;

namespace BusinessManager.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        string ValidateToken(string token);
    }
}
