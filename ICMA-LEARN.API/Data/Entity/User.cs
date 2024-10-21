using ICMA_LEARN.API.Data.Entity;

namespace ICMA_LEARN.API.Data.Entity
{
    public class User
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

}
