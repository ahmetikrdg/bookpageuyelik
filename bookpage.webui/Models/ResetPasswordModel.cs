using System.ComponentModel.DataAnnotations;

namespace bookpage.webui.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }//getle sayfaya taşiycaz post ederken tekrar göndericez
        [Required]
        [DataType(DataType.EmailAddress)]
         public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}