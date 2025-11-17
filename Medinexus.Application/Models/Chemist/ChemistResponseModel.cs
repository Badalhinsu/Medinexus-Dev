using Medinexus.Application.Models.User;

namespace Medinexus.Application.Models.Chemist
{
    public class ChemistResponseModel
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? MobileNo { get; set; }
        public string? GstNo { get; set; }
        public DateTime? GstIssueDate { get; set; }
        public string? TinNo { get; set; }
        public DateTime? TinIssueDate { get; set; }
        public string? CstNo { get; set; }
        public DateTime? CstIssueDate { get; set; }
        public string? DrugLicenceNo1 { get; set; }
        public string? DrugLicenceNo2 { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public UserResponseModel? UserDetails { get; set; }
    }
}
