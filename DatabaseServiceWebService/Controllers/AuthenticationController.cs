using System.Web.Http;
using DatabaseService.Queries.Mongodb;

namespace DatabaseServiceWebservice.Controllers {
  public class AuthenticationController : BaseController {
    [HttpPost]
    public dynamic Authenticate([FromBody] dynamic inputs) {
        
        string username = inputs.Username;
        logger.Debug("Checking authentication for user: " + username);
        return AuthenticateUserQuery.AuthenticateUser(username);
    }

    [HttpPost]
    public dynamic GetClient([FromBody] dynamic inputs)
    {

        int i = inputs.ClientId;
        logger.Debug("Retrieving client with id: " + i.ToString());
        return GetClientQuery.GetClient(i);
    }

    [HttpPost]
    public dynamic GetRefreshToken([FromBody] dynamic inputs)
    {
        //logger.DebugFormat("Checking authentication for refresh token: {0}", inputs.RefreshToken);
        return GetRefreshTokenQuery.GetRefreshToken(inputs.HashedToken);

    }

    [HttpPost]
    public dynamic SaveRefreshToken([FromBody] dynamic inputs)
    {
        SaveRefreshTokenQuery.SaveRefreshToken(inputs);
        return null;
    }

    [HttpPost]
    public dynamic DeleteRefreshToken([FromBody] dynamic inputs)
    {
        DeleteRefreshTokenQuery.DeleteRefreshToken(inputs.HashedToken);
        return null;
    }
  }
}
