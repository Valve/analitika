using System;
using Analitika.Data;
using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Net.NetworkInformation;

namespace Analitika {
  public partial class Auth : AppPage {
    private Connection _connection;
    private Action<string> _oauthCallback = null;

    private string _clientId = "";//TODO: here goes your clientId;
    private string _clientSecret = "";//TODO: here goes your client secret;
    public Auth() {
      InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e) {
      base.OnNavigatedTo(e);
      _connection = new Connection(_clientId, _clientSecret);
      _connection.ConnectionOpened += (o, evt) => {
        CurrentApp.Connection = _connection;
        NavigationService.Navigate(new Uri("/Profiles.xaml", UriKind.Relative));
      };
      _oauthCallback = _connection.OpenAsync(x => Browser.Navigate(x));
    }



    private void OnNavigationFailed(object sender, NavigationFailedEventArgs e) {
      if (e.Uri.AbsolutePath != "/o/oauth2/approval") {
        MessageBox.Show("Internet connection is not available");
      }
    }

    private void OnNavigating(object sender, NavigatingEventArgs e) {
      HideBrowser();
      if (e.Uri.Host == "localhost") {
        string queryString = e.Uri.Query.ToString();
        if (queryString.Contains("access_denied")) {
          MessageBox.Show("You must allow access in order to use Google Analytics");
          _oauthCallback = _connection.OpenAsync(x => Browser.Navigate(x));
        }
        else {
          string authCode = queryString.Substring(queryString.IndexOf("code=") + 5);
          _oauthCallback(authCode);
        }
      }
    }

    private void OnNavigated(object sender, NavigationEventArgs e) {
      if (e.Uri.AbsolutePath != "/ServiceLoginAuth") {
        ShowBrowser();
      }
    }

    private void ShowBrowser() {
      ProgressBar.Visibility = Visibility.Collapsed;
      Browser.Visibility = Visibility.Visible;
    }

    private void HideBrowser() {
      ProgressBar.Visibility = Visibility.Visible;
      Browser.Visibility = Visibility.Collapsed;
    }
  }
}
