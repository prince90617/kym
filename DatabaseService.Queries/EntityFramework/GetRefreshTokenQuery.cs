using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService.Queries.EntityFramework
{
    public static class GetRefreshTokenQuery
    {
        public static DatabaseService.Models.RefreshToken GetRefreshToken(string HashedToken)
         {

             using (var ctx = new SMContext())
             {
                 var refreshToken = from rft in ctx.RefreshTokens
                                    where rft.token == HashedToken
                                    select new DatabaseService.Models.RefreshToken { Token = rft.token, Username = rft.username, ClientId = rft.client_id, IssuedUtc = rft.issued_on, ExpiresUtc = rft.expires_on, ProtectedTicket = rft.protected_ticket };

                 return refreshToken.FirstOrDefault();
             }
             
         }
    }
}
