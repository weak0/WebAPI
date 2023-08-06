using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Authorization
{
    public class MinimumAgeRequirments: IAuthorizationRequirement
    {
        public int MinimumAge { get; set; }

        public MinimumAgeRequirments(int minage)
        {
            MinimumAge = minage;
        }
    }
}
