using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attachment : BaseEntity
    {
        public string FileUrl { get; set; }
        public int? ExpensesReportId { get; set; }
        public ExpensesReport? ExpensesReport { get; set; }
        public int? EncashmentId { get; set; }
        public Encashment? Encashment { get; set; }
        public DateTime UploadedDate { get; set; }
        public string UploadedBy { get; set; }
        public void SetDetails(string fileUrl, int encashmentId, DateTime uploadedDate, string uploadedBy)
        {
            FileUrl = fileUrl;
            EncashmentId = encashmentId;
            UploadedDate = uploadedDate;
            UploadedBy = uploadedBy;
        }
    }
}
