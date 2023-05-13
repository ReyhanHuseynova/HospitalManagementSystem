using System.ComponentModel.DataAnnotations;

namespace hospital.ViewModels
{
    public class ResetPasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]

        public string CheckPassword { get; set; }
    }
}
