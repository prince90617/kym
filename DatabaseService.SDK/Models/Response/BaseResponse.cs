using DatabaseService.SDK.Client;
using DatabaseService.SDK.Models.Request;
using Newtonsoft.Json;

namespace DatabaseService.SDK.Models.Response {
  public abstract class BaseResponse {
    public BaseResponse() {
      IsSuccess = false;
    }

    public bool IsSuccess { get; set; }
    public string ReasonPhrase { get; set; }
    public string Content { get; set; }

    public T ParseContent<T>() where T : class {
      return JsonConvert.DeserializeObject<T>(this.Content);
    }
  }
}
