using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagementSystemAPI.Data;
using UserManagementSystemAPI.Models;
using UserManagementSystemAPI.Models.Dto;
using UserManagementSystemAPI.Services;

namespace UserManagementSystemAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        readonly ApplicationDbContext _db;

        public AuthController(IConfiguration configuration, IUserService userService, ApplicationDbContext db)
        {
            _configuration = configuration;
            _userService = userService;
            _db = db;
        }

        public static string Decrypt(string encryptedText, int key)
        {
            string decrypted = "";
            foreach (char c in encryptedText)
            {
                int charCode = (int)c;
                int decryptedCharCode = charCode - key; // Subtract key from charCode
                decrypted += (char)decryptedCharCode;
            }
            return decrypted;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetUserClaims()
        {
            return Ok(_userService.GetUserClaims());
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Register(UserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null)
            {
                ModelState.AddModelError("CustomError", "Invalid Input");
                return BadRequest();
            }

            if (_db.Users.FirstOrDefault(u => u.Username.ToLower() == userRegisterDTO.Username.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Username already Exists");
                return BadRequest(ModelState);
            }

            if (_db.Users.FirstOrDefault(u => u.Email.ToLower() == userRegisterDTO.Email.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Email already Exists");
                return BadRequest(ModelState);
            }

            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password);

            User newUser = new()
            {
                Username = userRegisterDTO.Username,
                FName = userRegisterDTO.FName,
                LName = userRegisterDTO.LName,
                Email = userRegisterDTO.Email,
                Role = userRegisterDTO.Role,
                PasswordHash = passwordHash,
                CreatedDate = DateTime.Now
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return Ok(newUser);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Login(UserLoginDTO userLoginDTO)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == userLoginDTO.Username);

            if (user == null)
            {
                ModelState.AddModelError("CustomeError", "Invalid Username");
                return BadRequest(ModelState);
            }

            if (!BCrypt.Net.BCrypt.Verify(userLoginDTO.Password, user.PasswordHash))
            {
                ModelState.AddModelError("CustomeError", "Invalid Password");
                return BadRequest(ModelState);
            }

            string token = CreateToken(user);
            user.Token = token;

            return Ok(user);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.FName +" "+ user.LName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
        
        
}
