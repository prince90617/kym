using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService.Queries.EntityFramework
{
    public static class SaveRefreshTokenQuery
    {
        public static void SaveRefreshToken(dynamic inputs)
        {
            RefreshToken rft = new RefreshToken();

            rft.token = inputs.HashedToken;
            rft.username = inputs.Username;
            rft.client_id = inputs.ClientId;
            rft.expires_on = inputs.ExpiresUtc;
            rft.issued_on = inputs.IssuedUtc;
            rft.protected_ticket = inputs.ProtectedTicket;
            using (var ctx = new SMContext())
            {

                ctx.RefreshTokens.Add(rft);
                ctx.SaveChanges();

            }
        }
    }
}
