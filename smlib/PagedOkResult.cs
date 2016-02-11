using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace smlib
{
  public class PagedOkResult<T> : OkNegotiatedContentResult<T> {
    public PagedOkResult(T content, ApiController controller)
        : base(content, controller) { }

    public PagedOkResult(T content, IContentNegotiator contentNegotiator, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters) 
        : base(content, contentNegotiator, request, formatters) { }

    public int TotalResults { get; set; }

    public override async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.ExecuteAsync(cancellationToken);

        response.Headers.Add("X-RecordCount", TotalResults.ToString());

        return response;
    }  
  }
}
