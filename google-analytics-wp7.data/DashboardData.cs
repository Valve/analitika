using System;
using ServiceStack.Text;
using System.Globalization;

namespace Analitika.Data {
  public class DashboardData {

    private DashboardData() { }

    public int Visits { get; set; }
    public int UniqueVisitors { get; set; }
    public int PageViews { get; set; }
    public float PageViewsPerVisit { get; set; }
    public float AverageTimeOnSite { get; set; }
    public float PercentNewVisits { get; set; }

    public static DashboardData Empty {
      get {
        return new DashboardData();
      }
    }

    public static DashboardData Parse(string jsonResponse) {
      try {
        var formatProvider = new CultureInfo("en-US");
        var json = JsonSerializer.DeserializeFromString<JsonResponse<object>>(jsonResponse);
        var raw = json.rows[0];
        return new DashboardData {
          Visits = int.Parse(raw[0], formatProvider),
          UniqueVisitors = int.Parse(raw[1], formatProvider),
          PageViews = int.Parse(raw[2], formatProvider),
          PageViewsPerVisit = float.Parse(raw[3], formatProvider),
          AverageTimeOnSite = float.Parse(raw[4], formatProvider),
          PercentNewVisits = float.Parse(raw[5], formatProvider)
        };
      }
      catch {
        return DashboardData.Empty;
      }    
    }
  }
}
