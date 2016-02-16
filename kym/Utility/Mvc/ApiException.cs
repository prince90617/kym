using System;
using System.Net;

namespace kym.Utility.Mvc {
  public class ApiException : Exception {
    public ApiException(HttpStatusCode statusCode, string jsonData, string reason) {
      StatusCode = statusCode;
      JsonData = jsonData;
      ReasonPhrase = reason;
    }

    public HttpStatusCode StatusCode { get; private set; }
    public string JsonData { get; private set; }
    public string ReasonPhrase { get; private set; }
  }
}