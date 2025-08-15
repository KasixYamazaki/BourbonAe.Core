using System.ComponentModel.DataAnnotations;

namespace BourbonAe.Core.Models.Auth
{
    public class LoginViewModel
    {
        [Required, Display(Name = "ユーザーID")]
        public string UserId { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), Display(Name = "パスワード")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "サインインを維持する")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
