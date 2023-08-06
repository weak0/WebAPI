using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class AccountDto
    {
        public string Name { get; set; }
   
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
    
}
