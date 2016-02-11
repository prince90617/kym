using System.Collections;
using System.Linq;
using System.Web.Mvc;

namespace kym.Utility.Mvc {
  public static class Extensions {
    public static IEnumerable Errors(this ModelStateDictionary modelState) {
      if (!modelState.IsValid) {
        return modelState.ToDictionary(kvp => kvp.Key,
            kvp => kvp.Value.Errors
                            .Select(e => e.ErrorMessage).ToArray())
                            .Where(m => m.Value.Count() > 0);
      }
      return null;
    }
  }
}