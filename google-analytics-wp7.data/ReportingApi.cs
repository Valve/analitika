using System;
using System.Net;
using System.IO;

namespace Analitika.Data {
  public class ReportingApi {
    private readonly string _baseUrl = "https://www.googleapis.com/analytics/v3/data/ga";
    
    private readonly Connection _connection;

    internal ReportingApi(Connection connection) {
      _connection = connection;
    }

    #region Properties

    internal string BaseUrl {
      get {
        return _baseUrl;
      }
    }

    public int ProfileId { get; set; }

    #endregion

    #region Methods

    internal void ExecuteRawRequestAsync(string requestUrl, Action<string> onComplete) {
      var request = HttpWebRequest.CreateHttp(requestUrl);
      request.Headers["Authorization"] = string.Format("Bearer {0}", _connection.AccessToken);
      request.BeginGetResponse(asyncResult=> {
        var response = request.EndGetResponse(asyncResult);
        string responseBody = null;
        using(var streamReader = new StreamReader(response.GetResponseStream())){
          responseBody = streamReader.ReadToEnd();
        }
        onComplete(responseBody);
      }, null);
    }


    public Query Between(DateTime start, DateTime end) {
      return new Query(this).Between(start, end);
    }

    #endregion
  }
}
