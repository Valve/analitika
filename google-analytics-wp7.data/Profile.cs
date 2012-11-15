
using System;
namespace Analitika.Data {
  public class Profile {
    public int id { get; set; }
    public int accountId { get; set; }
    public string webPropertyId { get; set; }
    public string name { get; set; }
    public string currency { get; set; }
    public string webSiteUrl { get; set; }
    public DateTime created { get; set; }
    
  }
}
