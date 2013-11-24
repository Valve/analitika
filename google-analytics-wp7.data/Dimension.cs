using System;


namespace Analitika.Data {
  public class Dimension {
    private readonly string _dimensionString;
    public static readonly Dimension Empty = new Dimension("");
    public static readonly Dimension Browser = new Dimension("ga:browser");
    public static readonly Dimension Date = new Dimension("ga:date");
    public static readonly Dimension PagePath = new Dimension("ga:pagepath");

    protected Dimension(string dimensionString) {
      _dimensionString = dimensionString;
    }

    public static Dimension operator +(Dimension d1, Dimension d2) {
      return Add(d1, d2);
    }

    public static Dimension Add(Dimension d1, Dimension d2) {
      return new Dimension(string.Format("{0},{1}", d1._dimensionString, d2._dimensionString));
    }

    public static bool operator ==(Dimension d1, Dimension d2) {
      return d1._dimensionString == d2._dimensionString;
    }

    public static bool operator !=(Dimension d1, Dimension d2) {
      return !(d1 == d2);
    }

    public override bool Equals(object obj) {
      Dimension other = obj as Dimension;
      if (other == null) return false;
      return this == other;
    }

    public override int GetHashCode() {
      return _dimensionString.GetHashCode();
    }
  }
}
