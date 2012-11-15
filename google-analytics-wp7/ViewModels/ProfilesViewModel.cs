using System.Collections.Generic;
using System.Collections.ObjectModel;
using Analitika.Data;

namespace Analitika.ViewModels {
  public class ProfilesViewModel : ObservableCollection<Profile> {

    public ProfilesViewModel(IEnumerable<Profile> profiles) {
      foreach (var profile in profiles) {
        Add(profile);
      }
    }   
  }
}
