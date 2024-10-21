namespace ICMA_LEARN.API.Data.Entity
{
    public class CourseContent
    {
        public int CourseContentID { get; set; }
        public int? CourseID { get; set; }
        public string? FilePath { get; set; }
        public virtual Course? Course { get; set; }
    }
}
