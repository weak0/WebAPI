using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebAPI.Authorization
{
    public class MinimumAgeRequirmentHandler : AuthorizationHandler<MinimumAgeRequirments>
    {
        private ILogger<MinimumAgeRequirmentHandler> _logger;

        public MinimumAgeRequirmentHandler(ILogger<MinimumAgeRequirmentHandler> logger)
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirments requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);
            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            _logger.LogInformation($"User: {userEmail} with date of birth : {dateOfBirth}");
            if (dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today)
            {
                _logger.LogInformation("Auth succedded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation($"Auth faild");
            }
            
            return Task.CompletedTask;
        }
    }
}
