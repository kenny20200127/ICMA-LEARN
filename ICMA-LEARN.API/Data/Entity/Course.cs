namespace ICMA_LEARN.API.Data.Entity
{
    public class Course
    {
        public int CourseID { get; set; }
        public string? CourseName { get; set; }
        public string? Description { get; set; }
        public int? UserID { get; set; }
        public int? CategoryID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual User? User { get; set; }
        public virtual Category? Category { get; set; }
    }
}
