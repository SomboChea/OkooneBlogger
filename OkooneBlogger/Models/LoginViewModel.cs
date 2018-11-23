using System.ComponentModel.DataAnnotations;

namespace OkooneBlogger.Models
{
    public class LoginViewModel
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password), MinLength(5)]
        public string Password { get; set; }
    }
}