using System;
using System.Net;
using System.Diagnostics;

namespace Analitika.Data {
  public class Query {
    private DateTime _start;
    private DateTime _end;
    private Metric _metric;
    private Dimension _dimension;

    #region Properties

    private Metric CurrentMetric {
      get {
        if (_metric == null) _metric = Metric.Empty;
        return _metric;
      }
    }

    private Dimension CurrentDimension {
      get {
        if (_dimension == null) _dimension = Dimension.Empty;
        return _dimension;
      }
    }

    #endregion

    #region ctor

    private Query(DateTime start, DateTime end) {
      _start = start;
      _end = end;
    }

    #endregion


    #region Methods

    public static Query Between(DateTime start, DateTime end) {
      return new Query(start, end);
    }


    public Query Metrics(Func<Metric, Metric> func) {
      _metric = func(CurrentMetric);
      return this;
    }

    public Query Dimensions(Func<Dimension, Dimension> func) {
      _dimension = func(CurrentDimension);
      return this;
    }

    //public void GetDashboardDataAsync(Action<DashboardData> onComplete) {
    //  string dashboardUrl = BuildDashboardUrl();
    //  _reportingApi.ExecuteRawRequestAsync(dashboardUrl, responseBody => {
    //    var dashboardData = DashboardData.Parse(responseBody);
    //    onComplete(dashboardData);
    //  });
    //}

    //private string BuildDashboardUrl() {
    //  string ids = HttpUtility.HtmlEncode(string.Format("ga:{0}", _reportingApi.ProfileId));
    //  string metrics = (Metric.Visits + Metric.Visitors + Metric.PageViews + Metric.PageViewsPerVisit + Metric.AverageTimeOnSite + Metric.PercentNewVisits).HtmlEncoded;
    //  string url = string.Format("{0}?ids={1}&start-date={2}&end-date={3}&metrics={4}", _reportingApi.BaseUrl, ids, _start.ToString("yyyy-MM-dd"), _end.ToString("yyyy-MM-dd"), metrics);
    //  return url;
    //}

    #endregion
  }
}
