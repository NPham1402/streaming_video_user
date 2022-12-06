using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace streaming_video_user.Models
{
    public partial class LikeFilm
    {
        public string? IdUserFilm { get; set; }
        public string? IdUser { get; set; }
        public string? IdFilm { get; set; }

        public virtual Film? IdFilmNavigation { get; set; }
        public virtual User? IdUserNavigation { get; set; }
    }
}
