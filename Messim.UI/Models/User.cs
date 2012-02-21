using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Messim.UI.Models
{

    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        public virtual ICollection<User> Subscribents { get; set; }
    }
}
