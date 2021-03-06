﻿using System;
using System.ComponentModel.DataAnnotations;
using Computer.Models.System;

namespace Computer.Models.Computer
{
    public class ComputerUsingHistoryViewModel
    {
        public int ComputerUsingHistoryId { get; set; }

        public int ComputerId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string UserId { get; set; }

        public virtual ComputerViewModel Computer { get; set; }

        public virtual AppUserViewModel User { get; set; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập trạng thái")]
        public bool Status { set; get; }
    }

    public class ComputerUsingHistoryDetailViewModel
    {
        public int ComputerUsingHistoryId { get; set; }

        public int ComputerId { get; set; }

        public string ComputerCode { get; set; }

        public string ComputerName { get; set; }

        public int DeparmentTypeId { get; set; }

        public string DeparmentTypeCode { get; set; }

        public string DeparmentTypeName { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }

        [Required(ErrorMessage = "Yêu cầu nhập trạng thái")]
        public bool Status { set; get; }

        public TimeSpan? TotalHour => EndTime - StartTime;
    }
}