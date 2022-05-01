using System;
using System.Collections.Generic;

namespace streaming_video_user.Models
{
    public partial class Gerne
    {
        public string IdGer { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool StatusDelete { get; set; }
    }
}
