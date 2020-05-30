using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Computer.Models.Computer
{
    public class DeparmentTypeViewModel
    {
        public int DeparmentTypeId { get; set; }

        [StringLength(10, ErrorMessage = "DeparmentTypeCode không được quá 10 ký tự")]
        public string DeparmentTypeCode { get; set; }

        [StringLength(50, ErrorMessage = "DeparmentTypeName không được quá 10 ký tự")]
        public string DeparmentTypeName { get; set; }

        [StringLength(250, ErrorMessage = "DeparmentTypeDescription không được quá 250 ký tự")]
        public string DeparmentTypeDescription { get; set; }

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
    
    public class DeparmentTypeSelectListViewModel
    {
        public int DeparmentTypeId { get; set; }

        public string DeparmentTypeCode { get; set; }

        public string DeparmentTypeName { get; set; }
    }
}