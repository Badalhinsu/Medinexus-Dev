using System.ComponentModel.DataAnnotations.Schema;

namespace Medinexus.Domain.Entities
{
    [Table("chemistmaster")]
    public class ChemistMaster
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("companyname")]
        public string? CompanyName { get; set; }

        [Column("addressline1")]
        public string? AddressLine1 { get; set; }

        [Column("addressline2")]
        public string? AddressLine2 { get; set; }

        [Column("city")]
        public string? City { get; set; }

        [Column("pincode")]
        public string? Pincode { get; set; }

        [Column("mobileno")]
        public string? MobileNo { get; set; }

        [Column("gst_no")]
        public string? GstNo { get; set; }

        [Column("gst_issuedate")]
        public DateTime? GstIssueDate { get; set; }

        [Column("tin_no")]
        public string? TinNo { get; set; }

        [Column("tin_issuedate")]
        public DateTime? TinIssueDate { get; set; }

        [Column("cst_no")]
        public string? CstNo { get; set; }

        [Column("cst_issuedate")]
        public DateTime? CstIssueDate { get; set; }

        [Column("druglicenceno1")]
        public string? DrugLicenceNo1 { get; set; }

        [Column("druglicenceno2")]
        public string? DrugLicenceNo2 { get; set; }

        [Column("createdat")]
        public DateTime? CreatedAt { get; set; }

        [Column("modifiedat")]
        public DateTime? ModifiedAt { get; set; }
    }
}
