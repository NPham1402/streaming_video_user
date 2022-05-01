using System;
using System.Collections.Generic;

namespace streaming_video_user.Models
{
    public partial class User
    {
        public string IdUser { get; set; } = null!;
        public string? Name { get; set; }
        public short? Age { get; set; }
        public string? UrlImg { get; set; }
        public bool StatusDelete { get; set; }
    }
}
