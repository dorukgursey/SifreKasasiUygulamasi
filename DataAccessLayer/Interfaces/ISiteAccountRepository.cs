using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ISiteAccountRepository
    {
        SiteAccount GetSiteAccountById(int siteAccountId);
        IEnumerable<SiteAccount> GetSiteAccountsByUserId(string userId);
        void CreateSiteAccount(SiteAccount siteAccount);
        void UpdateSiteAccount(SiteAccount siteAccount);
        void DeleteSiteAccount(int siteAccountId);
    }
}
