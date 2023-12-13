using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicLibraryAPI2.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ElectronicLibraryAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Library_DatabaseContext _context;

        public UsersController(Library_DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        [HttpPost("SignIn")]
        public IActionResult Token([FromBody] User user)
        {
            Hashingcs hashingcs = new Hashingcs();
            if (_context.Users == null)
            {
                return NotFound();
            }

            object response = null;

            var users = _context.Users.ToList();
            foreach (var u in users)
            {
                if (u.Login == user.Login.Trim())
                {
                    bool b = false;
                    b = hashingcs.AreEqual(user.Password, u.Password, u.SaltPassword);
                    if (b == true)
                    {
                        var identity = GetIdentity(user.Login, u);
                        if (identity == null)
                        {
                            return BadRequest(new { errorText = "Invalid username or password." });
                        }

                        var now = DateTime.UtcNow;
                        // создание JWT-токена
                        var jwt = new JwtSecurityToken(
                        issuer: AuthToken.ISSUER,
                        audience: AuthToken.AUDIENCE,
                        notBefore: now,
                        claims: identity.Claims,
                                expires: now.Add(TimeSpan.FromMinutes(AuthToken.LIFETIME)),
                                signingCredentials: new SigningCredentials(AuthToken.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                        response = new
                        {
                            access_token = encodedJwt,
                            username = identity.Name,
                            role = u.RoleId,
                            id = u.IdUser
                        };

                    }
                    else
                    {
                        return Ok("Неуспешная авторизация!");
                    }
                }
            }


            if (response == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(response);
            }
        }

        private ClaimsIdentity GetIdentity(string username, User user) // изменить метод для использования объекта пользователя
        {
            if (user != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()),
        };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
        public class LoginRequest
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            Hashingcs hashingcs = new Hashingcs();

            if (_context.Users == null)
            {
                return NotFound();
            }

            object response = null;

            var users = _context.Users.ToList();
            foreach (var u in users)
            {
                if (u.Login == loginRequest.Login.Trim())
                {
                    bool b = false;
                    b = hashingcs.AreEqual(loginRequest.Password, u.Password, u.SaltPassword);
                    if (b == true)
                    {
                        var identity = GetIdentity(loginRequest.Login, u);
                        if (identity == null)
                        {
                            return BadRequest(new { errorText = "Invalid username or password." });
                        }

                        var now = DateTime.UtcNow;
                        // Создание JWT-токена
                        var jwt = new JwtSecurityToken(
                            issuer: AuthToken.ISSUER,
                            audience: AuthToken.AUDIENCE,
                            notBefore: now,
                            claims: identity.Claims,
                            expires: now.Add(TimeSpan.FromMinutes(AuthToken.LIFETIME)),
                            signingCredentials: new SigningCredentials(AuthToken.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                        response = new
                        {
                            access_token = encodedJwt,
                            username = identity.Name,
                            role = u.RoleId,
                            id = u.IdUser
                        };
                    }
                    else
                    {
                        return Ok("Неуспешная авторизация!");
                    }
                }
            }

            if (response == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(response);
            }
        }



        [HttpPost("refresh_token")]
        public async Task<ActionResult> RefreshToken(string access_token)
        {
            var login = GetTokenInfo(access_token);
            User user = _context.Users.FirstOrDefault(x => x.Login == login);

            var identity = GetIdentityRefresh(user.Login, user.Password);

            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создание JWT-токен
            var jwt = new JwtSecurityToken(
            issuer: AuthToken.ISSUER,
                    audience: AuthToken.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthToken.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthToken.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }

        private string GetTokenInfo(string token)
        {
            var t = new JwtSecurityTokenHandler().ReadJwtToken(token);

            return t.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
        }

        private ClaimsIdentity GetIdentityRefresh(string username, string password)
        {
            User person = _context.Users.FirstOrDefault(x => x.Login == username && x.Password == password.ToString());
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.RoleId.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        [HttpPut("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            Hashingcs hashingcs = new Hashingcs();

            // Verify the old password
            bool isOldPasswordCorrect = hashingcs.AreEqual(changePasswordRequest.OldPassword, user.Password, user.SaltPassword);

            if (!isOldPasswordCorrect)
            {
                return BadRequest(new { errorText = "Old password is incorrect." });
            }

            // Verify that the new password and repeated new password match
            if (changePasswordRequest.NewPassword != changePasswordRequest.RepeatedNewPassword)
            {
                return BadRequest(new { errorText = "New passwords do not match." });
            }

            // Generate a new salt for the new password
            string newSalt = hashingcs.CreateSalt(10);

            // Hash the new password with the new salt
            string newPasswordHash = hashingcs.GenerateHash(changePasswordRequest.NewPassword, newSalt);

            // Update the user's salt and password in the database
            user.SaltPassword = newSalt;
            user.Password = newPasswordHash;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(new { message = "Password updated successfully." });
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            Hashingcs hashingcs = new Hashingcs();

            if (_context.Users == null)
          {
              return Problem("Entity set 'Library_DatabaseContext.Users'  is null.");
          }

            user.SaltPassword = hashingcs.CreateSalt(10);
            user.Password = hashingcs.GenerateHash(user.Password, user.SaltPassword);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.IdUser }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
