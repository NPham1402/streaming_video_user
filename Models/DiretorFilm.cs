using System;
using System.Collections.Generic;

namespace streaming_video_user.Models
{
    public partial class DiretorFilm
    {
        public string? IdFilm { get; set; }
        public string? Id { get; set; }

        public virtual Film? IdFilmNavigation { get; set; }
        public virtual Director? IdNavigation { get; set; }
    }
}
