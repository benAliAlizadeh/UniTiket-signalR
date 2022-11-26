using UniTiKet_Model;

namespace UniTiket.Models
{
    public class ChatViewModel
    {
        public List<TiketViewModel> Tikets { get; set; }
        public List<Message> Messages { get; set; }

        public Tiket Tiket { get; set; }

    }
}
