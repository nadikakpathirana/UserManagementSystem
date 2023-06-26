namespace UserManagementSystemAPI.Models.Dto
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        
        public string Username { get; set; } = string.Empty;
        
        public string FName { get; set; } = string.Empty;
        
        public string Lname { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

    }
}
