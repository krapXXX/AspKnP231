using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AspKnP231.Models.Home
{
    public class HomeModelsFormModel
    {
        [FromForm(Name = "user-login")]
        [Required(ErrorMessage = "Логін обов'язковий")]
        public String UserLogin { get; set; } = null!;

        [FromForm(Name = "user-password")]
        [Required(ErrorMessage = "Пароль обов'язковий")]
        [MinLength(6, ErrorMessage = "Пароль має бути не менше 6 символів")]
    
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Пароль повинен містити цифру, малу та велику літери, а також спецсимвол")]
        public String UserPassword { get; set; } = null!;

        [FromForm(Name = "user-button")]
        public String UserButton { get; set; } = null!;
    }
}
/* Для моделей форм є принцип зв'язування: дані автоматично 
 * потрапляють до моделі за умови, що назва властивості збігається
 * з іменем під яким передаються дані (ім'я input)
 * Якщо збіг неможливий, зокрема, через "kebab-case", назва 
 * встановлюється через атрибут
 */