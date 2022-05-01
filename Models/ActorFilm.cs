using System;
using System.Collections.Generic;

namespace streaming_video_user.Models
{
    public partial class ActorFilm
    {
        public string? IdActor { get; set; }
        public string? IdFilm { get; set; }

        public virtual Actor? IdActorNavigation { get; set; }
        public virtual Film? IdFilmNavigation { get; set; }
    }
}
