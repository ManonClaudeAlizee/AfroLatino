namespace AfroLatino.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public string DescriptionCourte { get; set; } = null!;
        public string DescriptionLongue { get; set; } = null!;
        public string Lieu { get; set; } = null!;
    }
}