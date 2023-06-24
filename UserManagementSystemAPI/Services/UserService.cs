using System.Security.Claims;
using UserManagementSystemAPI.Model;

namespace UserManagementSystemAPI.Services
{
    // Creating a service for accessing user Security Claims
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserClaims()
        {
            var result = string.Empty;

            if (_httpContextAccessor.HttpContext is not null)
            {
                var user = _httpContextAccessor.HttpContext.User;
                result = user.FindFirstValue(ClaimTypes.Name);
                



                //var userName = User?.Identity?.Name;
                //var roleClaims = User?.FindAll(ClaimTypes.Role);
                //var roles = roleClaims?.Select(c => c.Value).ToList();
                //var roles2 = User?.Claims
                //    .Where(c => c.Type == ClaimTypes.Role)
                //    .Select(c => c.Value)
                //    .ToList();
                //return Ok(new { userName, roles, roles2 });

            }
            return result;
        }
    }
}
