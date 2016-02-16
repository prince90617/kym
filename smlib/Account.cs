using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smlib
{
    public class Account
    {
        //usercon
        public int Authenticate(string username, string password)
        {
           // var user = _db.tblDealersSettings.Where(tds => tds.DlrNo.Equals(dealerNumber)).FirstOrDefault();
            return (int)LoginStatus.Success;
        }
        public enum LoginStatus
        {
            Success=1,
            InvalidCredentials=2,
            AccountLocked=3
        }
    }
}
