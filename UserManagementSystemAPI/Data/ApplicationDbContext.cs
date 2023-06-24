using UserManagementSystemAPI.Model.Dto;

namespace UserManagementSystemAPI.Data
{
    public class ApplicationDbContext
    {
        public static List<UserDTO> userList = new List<UserDTO>
        {
            new UserDTO { Id = 1, Name = "nadika", Email = "nadikakpathirana@gmail.com", ProPic="/test/test"},
            new UserDTO { Id = 2, Name = "koshala", Email = "nadikakpathirana2@gmail.com", ProPic="/test/test"}
        };

    }
}
