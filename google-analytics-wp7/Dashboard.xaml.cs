using System;
using System.Windows;
using System.Windows.Navigation;
using Analitika.Data.Extensions;

namespace Analitika {
  public partial class Dashboard : AppPage {

    #region Fields

    private int _profileId;
    private string _profileName;
    private DateTime _startDate = DateTime.Now.AddMonths(-1); //by default - a month ago
    private DateTime _endDate = DateTime.Now;

    #endregion

    #region Ctor

    public Dashboard() {
      InitializeComponent();
    }

    #endregion

    #region Methods

    protected override void OnNavigatedTo(NavigationEventArgs e) {
      base.OnNavigatedTo(e);
      ReadQueryParameters();
      Reporting.ProfileId = _profileId;
      ProfileName.Text = _profileName;
      WebSiteUrl.Text = "{0} — {1}".FormatWith(_startDate.ToShortDateString(), _endDate.ToShortDateString());
      Reporting.Between(_startDate, _endDate).GetDashboardDataAsync(data => {
        Dispatcher.BeginInvoke(() => {
          DataContext = data;
          ProgressBar.Visibility = Visibility.Collapsed;
        });
      });
    }

    private void ReadQueryParameters() {
      _profileId = NavigationContext.QueryString["profileId"].ToInt();
      _profileName = NavigationContext.QueryString["profileName"];
      TryReadDates();

    }

    private void TryReadDates() {
      string startDateCandidate, endDateCandidate;
      if (NavigationContext.QueryString.TryGetValue("startDate", out startDateCandidate)) _startDate = DateTime.Parse(startDateCandidate);
      if (NavigationContext.QueryString.TryGetValue("endDate", out endDateCandidate)) _endDate = DateTime.Parse(endDateCandidate);
    }

    #endregion

    #region Event handlers

    private void MenuDateRangeClick(object sender, EventArgs e) {
      var uri = new Uri("/DateRange.xaml?startDate={0}&endDate={1}&profileId={2}&profileName={3}".FormatWith(_startDate, _endDate, _profileId, _profileName), UriKind.Relative);
      NavigationService.Navigate(uri);
    }

    #endregion
  }
}