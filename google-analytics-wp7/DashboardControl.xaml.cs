using System.Windows.Controls;
using System.Windows.Navigation;
using System;
using Analitika.Data.Extensions;

namespace Analitika {
  public partial class DashboardControl : AppControl {
    #region Fields
    private NavigationContext _navigationContext;
    private int _profileId;
    private string _profileName;
    private DateTime _startDate = DateTime.Now.AddDays(-1); //by default - showing data for yesterday
    private DateTime _endDate = DateTime.Now;

    #endregion
    
    public DashboardControl() {
      InitializeComponent();
    }

    # region Methods

    public void LoadData(NavigationContext navigationContext) {
      _navigationContext = navigationContext;
      ReadQueryParameters();
      Reporting.ProfileId = _profileId;
      //ProfileName.Text = _profileName;
      //WebSiteUrl.Text = "{0} — {1}".FormatWith(_startDate.ToShortDateString(), _endDate.ToShortDateString());
      Reporting.Between(_startDate, _endDate).GetDashboardDataAsync(data => {
        Dispatcher.BeginInvoke(() => {
          DataContext = data;
        });
      });
    }

    private void ReadQueryParameters() {
      _profileId = _navigationContext.QueryString["profileId"].ToInt();
      _profileName = _navigationContext.QueryString["profileName"];
      TryReadDates();

    }

    private void TryReadDates() {
      string startDateCandidate, endDateCandidate;
      if (_navigationContext.QueryString.TryGetValue("startDate", out startDateCandidate)) _startDate = DateTime.Parse(startDateCandidate);
      if (_navigationContext.QueryString.TryGetValue("endDate", out endDateCandidate)) _endDate = DateTime.Parse(endDateCandidate);
    }

    # endregion
  }
}
