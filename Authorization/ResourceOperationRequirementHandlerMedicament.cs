using Inzynierka_API.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Inzynierka_API.Authorization
{
    public class ResourceOperationRequirementHandlerMedicament : AuthorizationHandler<ResourceOperationRequirement, Medicament>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Medicament medicament)
        {
            if(requirement.Operation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(medicament.User.Id == int.Parse(userId))
            {
                context.Succeed(requirement);

            }
            return Task.CompletedTask;
        }



    }
}
