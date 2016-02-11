using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService.Queries.EntityFramework
{
    public static class GetClientQuery
    {
        public static DatabaseService.Models.Client GetClient(int ClientId)
        {

            using (var ctx = new SMContext())
            {
                var client = from c in ctx.Clients
                           where c.client_id == ClientId && c.active
                           select new DatabaseService.Models.Client { Id = c.client_id, Secret = c.client_secret, Name = c.name,  Active = c.active, RefreshTokenLifeTime = c.refresh_token_life_time, AllowedOrigin = c.allowed_origin };

                return client.FirstOrDefault();
            }
        }
    }
}
