using System;
using System.Collections.Generic;

namespace streaming_video_user.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Films = new HashSet<Film>();
        }

        public string IdAdmin { get; set; } = null!;
        public string? Name { get; set; }
        public string? Password { get; set; }
        public bool StatusDelete { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}
