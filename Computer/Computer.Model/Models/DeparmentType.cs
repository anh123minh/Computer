using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Computer.Model.Abstract;

namespace Computer.Model.Models
{
    [Table("DeparmentTypes")]
    public class DeparmentType : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeparmentTypeId { get; set; }

        [MaxLength(10)]
        public string DeparmentTypeCode { get; set; }

        [MaxLength(50)]
        public string DeparmentTypeName { get; set; }

        [MaxLength(250)]
        public string DeparmentTypeDescription { get; set; }

        public virtual ICollection<Computer> Computers { get; set; }
    }
}
