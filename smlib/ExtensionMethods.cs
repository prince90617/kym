using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace smlib {
  public static class ExtensionMethods {
    public static string FixUrlEnding(this string url) {
      if (!url.EndsWith("/")) {
        return url + "/";
      }
      return url;
    }

    public static string FixUrlBeginning(this string url) {
      if (!url.StartsWith("http")) {
        return "http://" + url;
      }
      return url;
    }

    /// <summary>
    /// Compute the distance between two strings.
    /// </summary>
    public static int LevenshteinDistance(this string s, string t) {
      int n = s.Length;
      int m = t.Length;

      // Used to memoize the costs.
      int[,] d = new int[n + 1, m + 1];

      // If the string is empty, return the other string's length.
      if (n == 0) {
        return m;
      }

      if (m == 0) {
        return n;
      }

      // Initialize the 2d array.
      for (int i = 0; i <= n; d[i, 0] = i++) {
      }

      for (int j = 0; j <= m; d[0, j] = j++) {
      }

      // Iterate other string s
      for (int i = 1; i <= n; i++) {
        // Iterate over string t
        for (int j = 1; j <= m; j++) {
          // Note if the letters are different
          int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

          // Dynamic programming!
          d[i, j] = Math.Min(
              Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
              d[i - 1, j - 1] + cost);
        }
      }
      // Return the minimum difference.
      return d[n, m];
    }

    public static IDictionary<string, string> Merge(this IEnumerable<IDictionary<string, string>> dictionaries) {
      var mergedDictionaries = dictionaries
        .SelectMany(dict => dict)
        .ToLookup(pair => pair.Key, pair => pair.Value)
        .ToDictionary(group => group.Key, group => group.First());
      return mergedDictionaries;
    }

    public static IDictionary<string, string> ToDictionary(this JArray array) {
      var keyValuePairs = array.ToObject<List<KeyValuePair<string, string>>>();
      var dictionary = keyValuePairs.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
      return dictionary;
    }

  }
}
