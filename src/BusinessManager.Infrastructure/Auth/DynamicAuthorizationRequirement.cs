using Microsoft.AspNetCore.Authorization;

namespace BusinessManager.Infrastructure.Auth
{
    public class DynamicAuthorizationRequirement : IAuthorizationRequirement
    {
        public string EndpointTag { get; }

        public DynamicAuthorizationRequirement(string endpointTag)
        {
            EndpointTag = endpointTag;
        }
    }

}
