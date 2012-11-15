using System.Windows;
using Analitika.Data;
using Microsoft.Phone.Controls;

namespace Analitika {
  public class AppPage : PhoneApplicationPage {
    protected App CurrentApp { get { return (App)Application.Current; } }
    public ManagementApi Management { get { return CurrentApp.Connection.ManagementApi; } }
    public ReportingApi Reporting { get { return CurrentApp.Connection.ReportingApi; } }

  }
}
