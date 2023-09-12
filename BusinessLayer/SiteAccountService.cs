using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SiteAccountService : ISiteAccountService
    {
        private readonly ISiteAccountRepository _siteAccountRepository;
        private readonly string _encryptionKey;

        public SiteAccountService(ISiteAccountRepository siteAccountRepository, string encryptionKey)
        {
            _siteAccountRepository = siteAccountRepository;
            _encryptionKey = encryptionKey;
        }
    
        public void CreateSiteAccount(SiteAccount siteAccount)
        {
            string encryptedPassword = Encrypt(siteAccount.EncryptedPassword, _encryptionKey);
            siteAccount.EncryptedPassword = encryptedPassword;
            _siteAccountRepository.CreateSiteAccount(siteAccount);
        }

        public void DeleteSiteAccount(int siteAccountId)
        {
            _siteAccountRepository.DeleteSiteAccount(siteAccountId);
        }

        public IEnumerable<SiteAccount> GetAllSiteAccounts()
        {

            var siteAccounts = _siteAccountRepository.GetAllSiteAccounts();
            foreach (var siteAccount in siteAccounts)
            {
                siteAccount.EncryptedPassword = Decrypt(siteAccount.EncryptedPassword, _encryptionKey);
            }
            return siteAccounts;
        }

        public SiteAccount GetSiteAccountById(int siteAccountId)
        {
            var siteAccount = _siteAccountRepository.GetSiteAccountById(siteAccountId);

            if (siteAccount != null)
            {
                siteAccount.EncryptedPassword = Decrypt(siteAccount.EncryptedPassword, _encryptionKey);
            }

            return siteAccount;
        }

        public void UpdateSiteAccount(SiteAccount siteAccount)
        {
            string encryptedPassword = Encrypt(siteAccount.EncryptedPassword, _encryptionKey);
            siteAccount.EncryptedPassword = encryptedPassword;
            _siteAccountRepository.UpdateSiteAccount(siteAccount);
        }
        private string Encrypt(string encryptedPassword, string encryptionKey)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
                aesAlg.IV = new byte[16]; // IV (Initialization Vector) is set to all zeros as an example; use a random IV in production.

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(encryptedPassword);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
        private string Decrypt(string encryptedPassword, string encryptionKey)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(encryptionKey);
                aesAlg.IV = new byte[16]; // IV (Initialization Vector) is set to all zeros as an example; use the same IV used for encryption in production.

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
