namespace UserManagementSystemAPI.Models.Dto
{
    public class UserRegisterDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FName { get; set; } = string.Empty;
        public string Lname { get; set; } = string.Empty;
    }
}
