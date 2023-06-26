using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementSystemAPI.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        public string Username { get; set; } = string.Empty;

        public string FName { get; set; } = string.Empty;

        public string LName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        [NotMapped]
        public string Token { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }
}
