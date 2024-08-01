namespace SectionDetails.Queries.Response
{
    public class GetSectionListResponse
    {
        public int TotalSectionCount { get; set; }
        public List<GetAllSectionQueryResponse> Sections { get; set; }
    }
}
