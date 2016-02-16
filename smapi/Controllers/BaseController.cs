using DatabaseService.SDK.Client;
//using DatabaseService.SDK.Models.Request.User;
using log4net;
using Microsoft.CSharp.RuntimeBinder;
using Ninject;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using smlib;
using log4net.Config;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace smapi.Controllers
{
   
  public abstract class BaseController : ApiController {
    protected static IKernel kernel;
    protected ILog logger;

    static BaseController() {
      kernel = new StandardKernel();

      kernel.Bind<ILog>().ToMethod(x => {
        var logName = ConfigurationManager.AppSettings["LoggingName"];
        return LogManager.GetLogger(logName);
      });
    }
       
    public BaseController() {
         logger = kernel.Get<ILog>();
    }

    protected string GetValueOrNull(GetValueDelegate getValueMethod) {
      try {
        return getValueMethod();
      } catch (RuntimeBinderException) {
        return null;
      }
    }

    protected delegate string GetValueDelegate();

    private string _currentUsername = null;
    protected string CurrentUsername {
      get {
        if (_currentUsername == null) {
          var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
          _currentUsername = principal.Claims.Where(c => c.Type == "sub").Single().Value;
        }
        return _currentUsername;
      }
    }

    private int _currentUserId = -1;
    protected int CurrentUserId {
      get {
        if (_currentUserId == -1) {
          var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
          _currentUserId = int.Parse(principal.Claims.Where(c => c.Type == "user_id").Single().Value);
        }
        return _currentUserId;
      }
    }

    private string _currentRole = null;
    protected string CurrentRole {
      get {
        if (string.IsNullOrWhiteSpace(_currentRole)) {
          var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
          _currentRole = principal.Claims.Where(c => c.Type == "role").Single().Value;
        }
        return _currentRole;
      }
    }

    //protected async Task<bool> UserIsInRole(int userId, string roleName) {
    //  var userClient = new UserClient();
    //  var roleRequest = new GetRoleForUserRequest { UserId = userId };
    //  var roleResponse = await userClient.GetRoleForUser(roleRequest);
    //  return roleResponse.Role == roleName;
    //}

    //protected async Task<bool> UserIsAdmin(int userId) {
    //  return !(await UserIsInRole(userId, "user"));
    //}

    //protected async Task<bool> UserIsUser(int userId) {
    //  return await UserIsInRole(userId, "user");
    //}

    protected PagedOkResult<T> PagedOk<T>(T content, int recordCount) {
      return new PagedOkResult<T>(content: content, controller: this) {
        TotalResults = recordCount
      };
    }
  }
}
