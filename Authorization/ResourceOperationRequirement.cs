using Microsoft.AspNetCore.Authorization;

namespace Inzynierka_API.Authorization
{
    public enum ResourceOperation
    { 
        Create,
        Read,
        Update,
        Delete
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation operation)
        {
            Operation = operation;
        }

        public ResourceOperation Operation { get; }
    }
}
