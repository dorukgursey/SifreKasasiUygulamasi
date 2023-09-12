using DataAccessLayer.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SiteAccountRepository : ISiteAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public SiteAccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateSiteAccount(SiteAccount siteAccount)
        {
            _context.SiteAccounts.Add(siteAccount);
            _context.SaveChanges();
        }

        public void DeleteSiteAccount(int siteAccountId)
        {
            var siteAccount = _context.SiteAccounts.FirstOrDefault(sa => sa.SiteId == siteAccountId);
            if (siteAccount != null)
            {
                _context.SiteAccounts.Remove(siteAccount);
                _context.SaveChanges();
            }
        }

        public IEnumerable<SiteAccount> GetAllSiteAccounts()
        {
            return _context.SiteAccounts.ToList();
        }

        public SiteAccount GetSiteAccountById(int siteAccountId)
        {
            return _context.SiteAccounts.FirstOrDefault(sa => sa.SiteId == siteAccountId);
        }

        public void UpdateSiteAccount(SiteAccount siteAccount)
        {
            _context.SiteAccounts.Update(siteAccount);
            _context.SaveChanges();
        }
    }
}
