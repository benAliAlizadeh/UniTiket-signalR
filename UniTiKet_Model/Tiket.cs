using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTiKet_Model
{
    public class Tiket
    {
        [Key]
        public int TiketId { get; set; }

        [Required]
        public string Title { get; set; }
        public DateTime CreatedTime { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public List<Message> Messages { get; set; }
    }
}
