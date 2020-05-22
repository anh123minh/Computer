using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Computer.Models.Computer
{
    public class DeparmentTypeViewModel
    {
        public int DeparmentTypeId { get; set; }

        public string DeparmentTypeCode { get; set; }

        public string DeparmentTypeName { get; set; }

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
}