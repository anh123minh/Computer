using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Computer.Models.Computer
{
    public class ComputerTypeViewModel
    {
        public int ComputerTypeId { get; set; }

        public string ComputerTypeCode { get; set; }

        public string ComputerTypeName { get; set; }

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
}