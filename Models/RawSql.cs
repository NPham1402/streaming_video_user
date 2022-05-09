using Microsoft.EntityFrameworkCore;
namespace streaming_video_user.Models
{
    public class UserSecurityClone
    {
        public string IdUser { get; set; } = null!;
        public string Email { get; set; }
        public string Password { get; set; }
        public bool StatusDelete { get; set; }
    }
    public class RawSql:DbContext
    {
        public DbSet<UserSecurityClone> UserSecurityClones { get; set; }
    }
}
