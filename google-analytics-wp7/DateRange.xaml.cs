using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Analitika.Data.Extensions;
using System.Globalization;

namespace Analitika {

  public partial class DateRange : AppPage {

    #region Fields

    private int _profileId;
    private string _profileName;
    private DateTime _startDate;
    private DateTime _endDate;
    private bool _skipReadingQueryStringDates;

    #endregion

    #region Ctor

    public DateRange() {
      InitializeComponent();
    }

    #endregion

    #region Event handlers

    private void YesterdayClick(object sender, RoutedEventArgs e) {
      _startDate = DateTime.Now.AddDays(-1);
      _endDate = DateTime.Now;
      NavigateToDashboard();

    }

    private void ThisWeekClick(object sender, RoutedEventArgs e) {
      DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
      _startDate = DateTime.Now.AddDays(firstDayOfWeek - DateTime.Now.DayOfWeek);
      _endDate = DateTime.Now;
      NavigateToDashboard();
    }

    private void ThisMonthClick(object sender, RoutedEventArgs e) {
      _startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
      _endDate = DateTime.Now;
      NavigateToDashboard();
    }

    private void ThisYearClick(object sender, RoutedEventArgs e) {
      _startDate = new DateTime(DateTime.Now.Year, 1, 1);
      _endDate = DateTime.Now;
      NavigateToDashboard();
    }

    private void StartDateChanged(object sender, DateTimeValueChangedEventArgs e) {
      _startDate = e.NewDateTime.Value;
      _skipReadingQueryStringDates = true;
    }

    private void EndDateChanged(object sender, DateTimeValueChangedEventArgs e) {
      _endDate = e.NewDateTime.Value;
      _skipReadingQueryStringDates = true;
    }

    private void OkClick(object sender, RoutedEventArgs e) {
      NavigateToDashboard();
    }

    #endregion

    #region Methods

    protected override void OnNavigatedTo(NavigationEventArgs e) {
      base.OnNavigatedTo(e);
      ReadQueryParameters();
      ShowNavigationDatesInUI();
    }

    private void ReadQueryParameters() {
      _profileId = NavigationContext.QueryString["profileId"].ToInt();
      _profileName = NavigationContext.QueryString["profileName"];
      if (!_skipReadingQueryStringDates) {
        TryReadDates();
      }

    }

    private void TryReadDates() {
      string startDateCandidate, endDateCandidate;
      if (NavigationContext.QueryString.TryGetValue("startDate", out startDateCandidate)) _startDate = DateTime.Parse(startDateCandidate);
      if (NavigationContext.QueryString.TryGetValue("endDate", out endDateCandidate)) _endDate = DateTime.Parse(endDateCandidate);
    }

    private void ShowNavigationDatesInUI() {
      StartDate.Value = _startDate;
      EndDate.Value = _endDate;
    }

    private void NavigateToDashboard() {
      var uri = new Uri("/Dashboard.xaml?startDate={0}&endDate={1}&profileId={2}&profileName={3}".FormatWith(_startDate, _endDate, _profileId, _profileName), UriKind.Relative);
      NavigationService.Navigate(uri);
    }

    #endregion

  }
}