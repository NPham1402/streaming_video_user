using System;
using System.Collections.Generic;

namespace streaming_video_user.Models
{
    public partial class GerneFilm
    {
        public string? IdGer { get; set; }
        public string? IdFilm { get; set; }

        public virtual Film? IdFilmNavigation { get; set; }
        public virtual Gerne? IdGerNavigation { get; set; }
    }
}
