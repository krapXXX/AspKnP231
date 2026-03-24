using AspKnP231.Data;
using AspKnP231.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AspKnP231.Controllers.Api
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(DataContext dataContext) : ControllerBase
    {
        private readonly DataContext _dataContext = dataContext;

        [HttpGet]
        public Object DoGet()
        {
            string? userJson = HttpContext.Session.GetString("UserAccess");
            if (string.IsNullOrEmpty(userJson))
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new { message = "Authentication required" };
            }

            var userAccess = JsonSerializer.Deserialize<UserAccess>(userJson);

            var adminRole = _dataContext.UserRoles.FirstOrDefault(r => r.Name == "Admin");

            if (userAccess == null || adminRole == null || userAccess.UserRoleId != adminRole.Id)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                return new { message = "Access denied. Admins only." };
            }

            return _dataContext.UserAccesses
                .Include(ua => ua.UserData)
                .Select(ua => new
                {
                    ua.Id,
                    ua.Login,
                    ua.CreatedAt,
                    Name = ua.UserData.Name,
                    Email = ua.UserData.Email,
                    AvatarUrl = string.IsNullOrEmpty(ua.AvatarFilename)
                        ? null
                        : $"{Request.Scheme}://{Request.Host}/Storage/Item/{ua.AvatarFilename}"
                })
                .ToList();
        }
    }
}