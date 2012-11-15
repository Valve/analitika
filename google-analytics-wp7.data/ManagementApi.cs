using System;
using System.Net;
using ServiceStack.Text;

namespace Analitika.Data {
  public class ManagementApi {
    private Connection _connection;
    private string _baseUrl = "https://www.googleapis.com/analytics/v3";
    internal ManagementApi(Connection connection) {
      _connection = connection;
    }

    public void GetProfilesAsync(Action<Profile[]> onComplete) {
      var profilesPath = "/management/accounts/~all/webproperties/~all/profiles";
      var request = HttpWebRequest.CreateHttp(string.Format("{0}{1}", _baseUrl, profilesPath));
      request.Headers["Authorization"] = string.Format("Bearer {0}", _connection.AccessToken);
      request.BeginGetResponse(result => {
        var response = request.EndGetResponse(result);
        var jsonResponse = JsonSerializer.DeserializeFromStream<JsonResponse<Profile>>(response.GetResponseStream());
        onComplete(jsonResponse.items);       
      }, null);
    }
  }
}
