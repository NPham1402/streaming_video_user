
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace streaming_video_user.Models
{
    public partial class UserSecurity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string IdUser { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool StatusDelete { get; set; }
        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
