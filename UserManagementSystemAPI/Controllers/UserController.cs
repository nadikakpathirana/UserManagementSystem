using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystemAPI.Data;
using UserManagementSystemAPI.Model;
using UserManagementSystemAPI.Model.Dto;

namespace UserManagementSystemAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            return Ok(ApplicationDbContext.userList);
        }

        [HttpGet("{id:int}", Name ="GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> GetUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var user = ApplicationDbContext.userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> CreateUser([FromBody]UserDTO userDTO) {
            if (userDTO == null) {
                return BadRequest();
            }
            
            if (ApplicationDbContext.userList.FirstOrDefault(u => u.Name.ToLower() == userDTO.Name.ToLower()) != null){
                ModelState.AddModelError("CustomError", "User Already Exists");
                return BadRequest(ModelState);
            }

            if (userDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            userDTO.Id = ApplicationDbContext.userList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            ApplicationDbContext.userList.Add(userDTO);

            return CreatedAtRoute("GetUser", new { id = userDTO.Id }, userDTO);
        }

        [HttpDelete("id:int", Name ="DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var user = ApplicationDbContext.userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            ApplicationDbContext.userList.Remove(user);
            return NoContent();
        }

        [HttpPut("id:int", Name ="UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(int id, [FromBody]UserDTO userDTO)
        {
            if (userDTO == null || id != userDTO.Id)
            {
                return BadRequest();
            }

            var user = ApplicationDbContext.userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = userDTO.Name;
            user.Email = userDTO.Email;
            user.ProPic = userDTO.ProPic;
            return NoContent();
        }

        [HttpPatch("id:int", Name = "UpdatePartialUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePatialUser(int id, JsonPatchDocument<UserDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var user = ApplicationDbContext.userList.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            patchDTO.ApplyTo(user, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

    }
}
