using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Computer.Model.Abstract;

namespace Computer.Model.Models
{
    [Table("Computers")]
    public class Computer : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComputerId { get; set; }

        [MaxLength(10)]
        public string ComputerCode { get; set; }

        [MaxLength(50)]
        public string ComputerName { get; set; }

        [MaxLength(250)]
        public string ComputerDescription { get; set; }

        [Required]
        public int ComputerTypeId { get; set; }

        [Required]
        public int ProducerTypeId { get; set; }

        [Required]
        public int DeparmentTypeId { get; set; }

        public bool? IsBusyNow { get; set; }

        [ForeignKey("ComputerTypeId")]
        public virtual ComputerType ComputerType { get; set; }

        [ForeignKey("ProducerTypeId")]
        public virtual ProducerType ProducerType { get; set; }

        [ForeignKey("DeparmentTypeId")]
        public virtual DeparmentType DeparmentType { get; set; }

        public virtual IEnumerable<ComputerUsingHistory> ComputerUsingHistories { get; set; }
    }
}
