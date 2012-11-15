using System;

namespace Analitika.Data {
    public class ConnectionOpenedEventArgs : EventArgs {
      public AccessTokenResponse Response { get; set; }
    }
}
