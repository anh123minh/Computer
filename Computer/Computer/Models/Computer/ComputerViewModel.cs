using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Computer.Models.Computer
{
    public class ComputerViewModel
    {
        public int ComputerId { get; set; }

        public string ComputerCode { get; set; }

        public string ComputerName { get; set; }

        public string ComputerDescription { get; set; }

        public int ComputerTypeId { get; set; }

        public int ProducerTypeId { get; set; }

        public int DeparmentTypeId { get; set; }

        public bool? IsBusyNow { get; set; }

        public virtual ComputerTypeViewModel ComputerType { get; set; }

        public virtual ProducerTypeViewModel ProducerType { get; set; }

        public virtual DeparmentTypeViewModel DeparmentType { get; set; }

        public virtual IEnumerable<ComputerUsingHistoryViewModel> ComputerUsingHistories { get; set; }

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