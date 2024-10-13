namespace ApplicationUserDetails.Querires.Response
{
    public class GetAllAppUserQueryResponse
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = null!;
        public List<int> RoleIds { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
