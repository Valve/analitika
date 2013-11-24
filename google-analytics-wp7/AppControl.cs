using System.Windows;
using System.Windows.Controls;
using Analitika.Data;
namespace Analitika {
  public class AppControl : UserControl{
    protected App CurrentApp { get { return (App)Application.Current; } }
    public ManagementApi Management { get { return CurrentApp.Connection.ManagementApi; } }
    public ReportingApi Reporting { get { return CurrentApp.Connection.ReportingApi; } }
  }
}
