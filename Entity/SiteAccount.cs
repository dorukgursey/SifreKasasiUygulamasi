using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class SiteAccount
    {
        [Key]
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteUsername { get; set; }
        public string SitePassword { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
