﻿using System;

namespace Analitika.Data.Extensions {
  public static class StringExtensions {
    public static string FormatWith(this string format, params object[] args) {
      return string.Format(format, args);
    }
  }
}
