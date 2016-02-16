using System.Security.Principal;

namespace kym.Utility.Mvc {
  public class ApiPrincipal : IPrincipal {

    public ApiPrincipal(ApiIdentity identity, string role) {
      Identity = identity;
      Role = role;
    }

    public IIdentity Identity {
      get;
      private set;
    }

    public string Role {
      get;
      private set;
    }

    public bool IsInRole(string role) {
      return role == Role;
    }
  }
}