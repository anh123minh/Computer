using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Computer.Model.Abstract;

namespace Computer.Model.Models
{
    [Table("ProducerTypes")]
    public class ProducerType : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProducerTypeId { get; set; }

        [MaxLength(10)]
        public string ProducerTypeCode { get; set; }

        [MaxLength(50)]
        public string ProducerTypeName { get; set; }

        [MaxLength(250)]
        public string ProducerTypeDescription { get; set; }

        public virtual ICollection<Computer> Computers { get; set; }
    }
}
