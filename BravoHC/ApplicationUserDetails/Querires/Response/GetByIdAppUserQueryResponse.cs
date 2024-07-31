namespace ApplicationUserDetails.Querires.Response
{
    public class GetByIdAppUserQueryResponse
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}