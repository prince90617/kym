using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseService.Queries.EntityFramework
{
    public static class DeleteRefreshTokenQuery
    {
        public static void DeleteRefreshToken(string HashedToken)
         {
             RefreshToken tokenToDelete;

             using (var ctx = new SMContext())
             {
                 tokenToDelete = ctx.RefreshTokens.Where(s => s.token == HashedToken).FirstOrDefault<RefreshToken>();
             }

             //Create new context for disconnected scenario
             using (var newContext = new SMContext())
             {
                 newContext.Entry(tokenToDelete).State = System.Data.Entity.EntityState.Deleted;

                 newContext.SaveChanges();
             }  
            
         }
    }
}
