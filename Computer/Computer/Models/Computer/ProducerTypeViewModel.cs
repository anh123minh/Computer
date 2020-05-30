using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Computer.Models.Computer
{
    public class ProducerTypeViewModel
    {
        public int ProducerTypeId { get; set; }

        [StringLength(10, ErrorMessage = "ProducerTypeCode không được quá 10 ký tự")]
        public string ProducerTypeCode { get; set; }

        [StringLength(50, ErrorMessage = "ProducerTypeName không được quá 50 ký tự")]
        public string ProducerTypeName { get; set; }

        [StringLength(250, ErrorMessage = "ProducerTypeDescription không được quá 250 ký tự")]
        public string ProducerTypeDescription { get; set; }

        public virtual IEnumerable<ComputerViewModel> Computers { get; set; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập trạng thái")]
        public bool Status { set; get; }
    }

    public class ProducerTypeSelectListViewModel
    {
        public int ProducerTypeId { get; set; }

        public string ProducerTypeCode { get; set; }

        public string ProducerTypeName { get; set; }
    }
}