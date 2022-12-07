using System;
using System.Collections.Generic;

namespace streaming_video_user.Models
{
    public abstract class FilmAbstract
    {

    }
    public partial class Film:FilmAbstract
    {
        public string IdFilm { get; set; } = null!;
        public string? IdAdmin { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }
        public string? UrlImg { get; set; }
        public string? UrlFilm { get; set; }
        public string? YearPublic { get; set; }
        public string? AgeLimit { get; set; }
        public bool? StatusDelete { get; set; }
        public virtual Admin? IdAdminNavigation { get; set; }
    }
}
