using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ISiteAccountService
    {
        SiteAccount GetSiteAccountById(int siteAccountId);
        IEnumerable<SiteAccount> GetAllSiteAccounts();
        void CreateSiteAccount(SiteAccount siteAccount);
        void UpdateSiteAccount(SiteAccount siteAccount);
        void DeleteSiteAccount(int siteAccountId);
    }
}
