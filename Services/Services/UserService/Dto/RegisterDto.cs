using System.ComponentModel.DataAnnotations;

namespace Services.Services.UserService.Dto
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { set; get; }
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$" , ErrorMessage ="Password must have 1 UpperCase , 1 lowerCase , 1 number , 1 non-alphanumeric and at least 6 characters")]
        public string Password { set; get; }
    }
}
