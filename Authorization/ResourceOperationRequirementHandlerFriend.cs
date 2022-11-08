using Inzynierka_API.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Inzynierka_API.Authorization
{
    public class ResourceOperationRequirementHandlerFriend : AuthorizationHandler<ResourceOperationRequirement, Friend>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Friend friend)
        {
            if(requirement.Operation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(friend.Id == int.Parse(userId))
            {
                context.Succeed(requirement);

            }
            return Task.CompletedTask;
        }



    }
}
