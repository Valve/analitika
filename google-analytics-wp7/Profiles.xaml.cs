using System;
using System.Windows.Navigation;
using Analitika.Data;
using Analitika.Data.Extensions;
using Analitika.ViewModels;

namespace Analitika {
  public partial class Profiles : AppPage {
    public Profiles() {
      InitializeComponent();
    }
    protected override void OnNavigatedTo(NavigationEventArgs e) {
      base.OnNavigatedTo(e);
      Management.GetProfilesAsync(profiles => {
        Dispatcher.BeginInvoke(() => {
          profilesList.ItemsSource = new ProfilesViewModel(profiles);
        });
      });
    }

    private void OnProfileSelected(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
      if (profilesList.SelectedIndex < 0) return;
      var profile = (Profile)e.AddedItems[0];
      var uri = new Uri("/Dashboard.xaml?profileId={0}&profileName={1}&webSiteUrl={2}".FormatWith(profile.id, profile.name, profile.webSiteUrl), UriKind.Relative);
      NavigationService.Navigate(uri);
    }
  }
}