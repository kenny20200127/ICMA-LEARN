namespace ICMA_LEARN.API.DataModel.CourseModel
{

    public class CourseContentDataModel
    {
        public int CourseContentID { get; set; }
        public string FilePath { get; set; }
    }

    public class CourseDataModel
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
