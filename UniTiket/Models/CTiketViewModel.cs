using UniTiKet_Model;
using System.ComponentModel.DataAnnotations;

namespace UniTiket.Models
{
    public class CTiketViewModel
    {
        [Reguierd]
        public string Category { get; set; }

        [Reguierd]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public List<Category>? Categories { get; set; }
    }
    public class ETiketViewModel
    {
        [Reguierd]
        public int TiketId { get; set; }

        [Reguierd]
        public string Category { get; set; }

        [Reguierd]
        public string Title { get; set; }

        [Required]
        public bool IsFinaly { get; set; }

        public List<Category>? Categories { get; set; }
    }
}