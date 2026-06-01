using System.ComponentModel.DataAnnotations;

namespace EShop.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Please enter your name.....")]
        public string Name { get; set; }
    }
}
