using System.ComponentModel.DataAnnotations;

namespace DatingApi.DTOs
{
    public class RegisterDto
    {
      [Required]
      public string Username { get; set; }
      [Required]
      [StringLength(18,MinimumLength=4)]
      public string Password { get; set; }
    }
}