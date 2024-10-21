namespace ICMA_LEARN.API.Data.Entity
{
    public class Attempt
    {
        public int AttemptID { get; set; }
        public int? UserID { get; set; }
        public int? CourseID { get; set; }
        public decimal? Score { get; set; }
        public string? CompletionStatus { get; set; }
        public DateTime? AttemptDate { get; set; }
        public virtual User? User { get; set; }
        public virtual Course? Course { get; set; }
    }

}
