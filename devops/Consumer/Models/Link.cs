namespace Consumer.Models
{
    public class Link
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; } = null;
        public DateTime? DeleteDate { get; set; } = null;
        public string Url { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
    }
}
