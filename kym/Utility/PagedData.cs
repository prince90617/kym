using Newtonsoft.Json;

namespace kym.Utility {
  public class PagedData<T> {
    [JsonProperty("data")]
    public T Data { get; set; }
    [JsonProperty("itemsCount")]
    public int ItemCount { get; set; }
  }
}