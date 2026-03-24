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

        // Допоміжний метод для перевірки, чи є поточний користувач адміністратором
        private bool IsAdmin()
        {
            string? userJson = HttpContext.Session.GetString("UserAccess");
            if (string.IsNullOrEmpty(userJson)) return false;

            var userAccess = JsonSerializer.Deserialize<UserAccess>(userJson);
            var adminRole = _dataContext.UserRoles.FirstOrDefault(r => r.Name == "Admin");

            return userAccess != null && adminRole != null && userAccess.UserRoleId == adminRole.Id;
        }

        [HttpGet]
        public Object DoGet()
        {
            if (!IsAdmin())
            {
                Response.StatusCode = HttpContext.Session.GetString("UserAccess") == null ? 401 : 403;
                return new { message = "Access denied" };
            }

            return _dataContext.UserAccesses
                .Include(ua => ua.UserData)
                .Select(ua => new {
                    ua.Id,
                    ua.Login,
                    ua.CreatedAt,
                    Name = ua.UserData.Name,
                    Email = ua.UserData.Email
                })
                .ToList();
        }

        // НОВИЙ МЕТОД: Отримання одного користувача за ID або Логіном
        [HttpGet("{idOrLogin}")]
        public Object DoGetSingle(string idOrLogin)
        {
            // 1. Перевірка прав доступу (копіюємо логіку з DoGet)
            if (!IsAdmin())
            {
                Response.StatusCode = HttpContext.Session.GetString("UserAccess") == null ? 401 : 403;
                return new { message = "Access denied" };
            }

            // 2. Пошук користувача
            // Спробуємо знайти або за Login, або за Id (якщо рядок можна перетворити в Guid)
            UserAccess? user = _dataContext.UserAccesses
                .Include(ua => ua.UserData)
                .FirstOrDefault(ua => ua.Login == idOrLogin || ua.Id.ToString() == idOrLogin);

            // 3. Якщо не знайдено — 404
            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new { message = "User not found" };
            }

            // 4. Повертаємо безпечний об'єкт
            return new
            {
                user.Id,
                user.Login,
                user.CreatedAt,
                user.AvatarFilename,
                Name = user.UserData.Name,
                Email = user.UserData.Email,
                Birthdate = user.UserData.Birthdate
            };
        }
    }
}