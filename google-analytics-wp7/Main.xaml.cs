using System;
using System.Windows;
using System.Windows.Navigation;
using Analitika.Data.Extensions;

namespace Analitika {
  public partial class Main : AppPage {


    #region Ctor

    public Main() {
      InitializeComponent();
    }

    #endregion

    #region Methods

    protected override void OnNavigatedTo(NavigationEventArgs e) {
      base.OnNavigatedTo(e);
      Dashboard.LoadData(NavigationContext);
    }

    #endregion

    #region Event handlers

    private void MenuDateRangeClick(object sender, EventArgs e) {
      //var uri = new Uri("/DateRange.xaml?startDate={0}&endDate={1}&profileId={2}&profileName={3}".FormatWith(_startDate, _endDate, _profileId, _profileName), UriKind.Relative);
      //NavigationService.Navigate(uri);
    }

    #endregion
  }
}