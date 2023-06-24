using System.ComponentModel.DataAnnotations;

namespace UserManagementSystemAPI.Model.Dto
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Key]
        public string? Email { get; set; }

        public string? ProPic { get; set; }



        

    }
}
