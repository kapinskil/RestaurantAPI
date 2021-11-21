using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace RestaurantAPI.Authorization
{
    public class MinimumRangeRequrmentHandler : AuthorizationHandler<MinimumRangeRequrment>
    {
        private readonly ILogger<MinimumRangeRequrmentHandler> _logger;

        public MinimumRangeRequrmentHandler(ILogger<MinimumRangeRequrmentHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRangeRequrment requirement)
        {
            var userEmial = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            _logger.LogInformation($"user {userEmial} try authorize");

            if(dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Now)
            {
                _logger.LogInformation("Autorization succesed");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Autorization faild");
            }

            return Task.CompletedTask;
            
        }
    }
}
