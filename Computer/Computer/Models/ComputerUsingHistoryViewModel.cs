using System;
using System.ComponentModel.DataAnnotations;

namespace Computer.Models
{
    public class ComputerUsingHistoryViewModel
    {
        public int ComputerUsingHistoryId { get; set; }

        public int ComputerId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string UserId { get; set; }

        public virtual ComputerViewModel Computer { get; set; }

        //public virtual AppUserViewModel User { get; set; }

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