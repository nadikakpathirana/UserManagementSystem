namespace UserManagementSystemAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FName { get; set; } = string.Empty;
        public string Lname { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }


    }
}
