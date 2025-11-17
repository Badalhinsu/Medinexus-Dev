namespace Medinexus.Application.Models.User
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
