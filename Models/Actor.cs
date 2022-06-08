using System;
using System.Collections.Generic;

namespace streaming_video_user.Models
{
    public partial class Actor
    {
        public string IdActor { get; set; } = null!;
        public string? Name{ get; set; }
        public string? Description { get; set; }
        public string? UrlImg { get; set; }
        public bool StatusDelete { get; set; }

    }
}
