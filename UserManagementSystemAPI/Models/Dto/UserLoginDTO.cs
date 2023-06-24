using System.ComponentModel.DataAnnotations;

namespace UserManagementSystemAPI.Models.Dto
{
    public class UserLoginDTO
    {
        [Required]
        public string Username { get; set; }

        // plain text
        [Required]
        public string? Password { get; set; }
    }
}
