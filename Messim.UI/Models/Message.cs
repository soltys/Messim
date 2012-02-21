using System;
using System.ComponentModel.DataAnnotations;

namespace Messim.UI.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public virtual User UserID { get; set; }
        public DateTime Date { get; set; }
        public int LikeAmount { get; set; }

    }
}