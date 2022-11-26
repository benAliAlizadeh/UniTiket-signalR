using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTiKet_Model
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }

        public int TiketId { get; set; }
        public bool IsAnser { get; set; }

        [ForeignKey("TiketId")]
        public Tiket Tiket { get; set; }
    }
}
