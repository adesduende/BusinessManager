namespace BusinessManager.Infrastructure.Auth
{
    public class JwtSettings
    {
        public string SecretKey { get; init; } = default!;
        public string Issuer { get; init; } = "TaskFlowAPI";
        public string Audience { get; init; } = "TaskFlowClient";
        public int ExpiryMinutes { get; init; } = 60;
    }
}
