using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystemAPI.Data;
using UserManagementSystemAPI.Model.Dto;
using UserManagementSystemAPI.Models;
using UserManagementSystemAPI.Models.Dto;

namespace UserManagementSystemAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet, Authorize(Roles = "Admin,Staff")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            IEnumerable<User> objUsersList = _db.Users.ToList();
            return Ok(objUsersList);
        }

        [HttpGet("{id:int}", Name ="GetUser"), Authorize(Roles = "Admin,Staff,User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> GetUser(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("CustomError", "Invalid Id");
                return BadRequest(ModelState);
            }

            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                ModelState.AddModelError("CustomError", "User does not exists");
                return NotFound(ModelState);
            }

            return Ok(user);
        }

        [HttpDelete("id:int", Name ="DeleteUser"), Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                ModelState.AddModelError("CustomError", "Already does not exists");
                return NotFound(ModelState);
            }
            _db.Users.Remove(user);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("id:int", Name ="UpdateUser"), Authorize( Roles ="Admin,Staff,User")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(int id, UserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null)
            {
                return BadRequest();
            }

            var user = _db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.FName = userRegisterDTO.FName;
            user.LName = userRegisterDTO.LName;
            user.Email = userRegisterDTO.Email;
            user.Role = userRegisterDTO.Role;

            _db.SaveChanges();
            
            return Ok(user);
        }
    }
}
