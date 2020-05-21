using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Computer.Model.Abstract;

namespace Computer.Model.Models
{
    [Table("ComputerUsingHistories")]
    public class ComputerUsingHistory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComputerUsingHistoryId { get; set; }

        [Required]
        public int ComputerId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [Required]
        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string UserId { get; set; }

        [ForeignKey("ComputerId")]
        public virtual Computer Computer { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
    }
}
