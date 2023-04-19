namespace AfroLatino.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime DatePublication { get; set; }
        public string DescriptionCourte { get; set; } = null!;
        public string Type { get; set; } = null!; //bachata, salsa portoricaine, salsa cubaine ou kizomba
        public string? SourceVideo { get; set; } = null!;
    }
}