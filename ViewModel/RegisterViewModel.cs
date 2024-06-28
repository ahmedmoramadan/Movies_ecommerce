using System.ComponentModel.DataAnnotations;

namespace movies_ecommerce.ViewModel
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirm { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? email { get; set; }
        public string num { get; set; }
        public string address { get; set; }
    }
}
