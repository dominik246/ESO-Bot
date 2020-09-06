using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotRepository.Models
{
    public class GuildTableModel
    {
        [Key]
        public int Id { get; set; }

        public uint Channel { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        [MaxLength(300)]
        public string MagickaDps { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50)]
        public string MagickaDpsLogo { get; set; } = ":comet:";

        [Column(TypeName = "nvarchar(300)")]
        [MaxLength(300)]
        public string StaminaDps { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50)]
        public string StaminaDpsLogo { get; set; } = ":crossed_swords:";

        [Column(TypeName = "nvarchar(300)")]
        [MaxLength(300)]
        public string Healers { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50)]
        public string HealersLogo { get; set; } = ":yellow_heart:";

        [Column(TypeName = "nvarchar(300)")]
        [MaxLength(300)]
        public string Tanks { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50)]
        public string TanksLogo { get; set; } = ":shield:";

        [Column(TypeName = "nvarchar(300)")]
        [MaxLength(300)]
        public string BackUps { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50)]
        public string BackUpsLogo { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        [MaxLength(300)]
        public string EmbedDescription { get; set; }

        [Column(TypeName = "nvarchar(600)")]
        [MaxLength(600)]
        public string EventReminder { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime EndsAt { get; set; }

        [Column(TypeName = "DateTimeOffset")]
        public DateTimeOffset TimeRemaining { get; set; }
    }
}
