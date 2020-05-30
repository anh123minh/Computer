using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Computer.Models.Computer
{
    public class ComputerTypeViewModel
    {
        public int ComputerTypeId { get; set; }

        [StringLength(10, ErrorMessage = "ComputerTypeCode không được quá 10 ký tự")]
        public string ComputerTypeCode { get; set; }

        [StringLength(50, ErrorMessage = "ComputerTypeName không được quá 50 ký tự")]
        public string ComputerTypeName { get; set; }

        [StringLength(250, ErrorMessage = "ComputerTypeDescription không được quá 250 ký tự")]
        public string ComputerTypeDescription { get; set; }

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

    public class ComputerTypeSelectListViewModel
    {
        public int ComputerTypeId { get; set; }

        public string ComputerTypeCode { get; set; }

        public string ComputerTypeName { get; set; }
    }
}