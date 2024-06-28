using System.ComponentModel.DataAnnotations;

namespace movies_ecommerce.ViewModel
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
       [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
