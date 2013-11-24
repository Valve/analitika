using System;
using System.IO;
using System.Net;
using ServiceStack.Text;

namespace Analitika.Data {

  public class Connection {

    #region Fields

    string _scheme = "https://";
    string _host = "accounts.google.com";
    string _authenticationPath = "/o/oauth2/auth";
    string _tokenPath = "/o/oauth2/token";
    string _scope = "https://www.googleapis.com/auth/analytics.readonly";
    string _redirectUri = HttpUtility.UrlEncode("http://localhost");
    private readonly string _baseUrl = "https://www.googleapis.com/analytics/v3/data/ga";
    private string _clientId;
    private string _clientSecret;
    private AccessTokenResponse _accessTokenResponse = AccessTokenResponse.Empty;
    private ManagementApi _managementApi;
    public event EventHandler<ConnectionOpenedEventArgs> ConnectionOpened;

    #endregion

    #region Constructor

    public Connection(string clientId, string clientSecret) {
      _clientId = clientId;
      _clientSecret = clientSecret;
    }

    #endregion

    #region Properties

    public ManagementApi ManagementApi { get { return _managementApi; } }

    #endregion

    #region Methods

    internal void ExecuteRawRequestAsync(string requestUrl, Action<string> onComplete) {
      var request = HttpWebRequest.CreateHttp(requestUrl);
      request.Headers["Authorization"] = string.Format("Bearer {0}", _accessTokenResponse.access_token;
      request.BeginGetResponse(asyncResult=> {
        var response = request.EndGetResponse(asyncResult);
        string responseBody = null;
        using(var streamReader = new StreamReader(response.GetResponseStream())){
          responseBody = streamReader.ReadToEnd();
        }
        onComplete(responseBody);
      }, null);
    }


    public Action<string> OpenAsync(Action<Uri> oauthStartCallback) {
      Uri authenticationUri = BuildAuthenticationUri();
      oauthStartCallback(authenticationUri);
      var callback = new Action<string>(OAuthCallback);
      return callback;
    }

    public void OAuthCallback(string authenticationCode) {
      if (string.IsNullOrEmpty(authenticationCode)) throw new ArgumentException("authenticationCode can't be null or empty");
      ObtainAccessTokenResponseAndRaiseOpenEvent(authenticationCode);
    }

    private void ObtainAccessTokenResponseAndRaiseOpenEvent(string authenticationCode) {
      var accessTokenUrl = BuildAccessTokenUri();
      var webClient = new WebClient();
      webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
      string data = string.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code", authenticationCode, _clientId, _clientSecret, _redirectUri);
      webClient.UploadStringCompleted += (o, e) => {
        _accessTokenResponse = JsonSerializer.DeserializeFromString<AccessTokenResponse>(e.Result);
        _managementApi = new ManagementApi(this);
        if (ConnectionOpened != null) {
          ConnectionOpened(this, new ConnectionOpenedEventArgs { Response = _accessTokenResponse });
        }
      };
      webClient.UploadStringAsync(accessTokenUrl, data);
    }

    private Uri BuildAuthenticationUri() {
      var uriBuilder = new UriBuilder();
      uriBuilder.Scheme = _scheme;
      uriBuilder.Host = _host;
      uriBuilder.Path = _authenticationPath;
      uriBuilder.Query = string.Format("response_type=code&client_id={0}&clientSecret={1}&scope={2}&redirect_uri={3}", _clientId, _clientSecret, _scope, _redirectUri);
      return uriBuilder.Uri;
    }

    private Uri BuildAccessTokenUri() {
      var uriBuilder = new UriBuilder();
      uriBuilder.Scheme = _scheme;
      uriBuilder.Host = _host;
      uriBuilder.Path = _tokenPath;
      return uriBuilder.Uri;
    }

    #endregion

  }
}
