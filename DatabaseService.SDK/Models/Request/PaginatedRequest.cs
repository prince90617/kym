namespace DatabaseService.SDK.Models.Request {
    public class PaginatedRequest : BaseRequest {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
    }
}
