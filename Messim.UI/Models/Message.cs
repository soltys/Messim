using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Messim.UI.Models
{
    public class Message
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }

        public virtual Image Image { get; set; }

        public virtual User Sender { get; set; }
        public DateTime Date { get; set; }
        public virtual List<User> WhoLikes { get; set; }

        public virtual Message ReplyTo { get; set; }
    }
}