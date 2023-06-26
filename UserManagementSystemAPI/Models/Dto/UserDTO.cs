using System.ComponentModel.DataAnnotations;

namespace UserManagementSystemAPI.Model.Dto
{
    public class UserDTO
    {
        public string FName { get; set; } = string.Empty;

        public string LName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

    }
}
