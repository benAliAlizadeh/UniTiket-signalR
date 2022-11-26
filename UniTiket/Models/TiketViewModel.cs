namespace UniTiket.Models
{
    public class TiketViewModel
    {
        public int TiketId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedTime { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public string LastMessage { get; set; }
        public int MessageCount { get; set; }
    }
}
