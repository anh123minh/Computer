using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Computer.Model.Abstract;

namespace Computer.Model.Models
{
    [Table("ComputerTypes")]
    public class ComputerType : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ComputerTypeId { get; set; }

        [MaxLength(10)]
        public string ComputerTypeCode { get; set; }

        [MaxLength(50)]
        public string ComputerTypeName { get; set; }

        [MaxLength(250)]
        public string ComputerTypeDescription { get; set; }

        public virtual IEnumerable<Computer> Computers { get; set; }
    }
}
