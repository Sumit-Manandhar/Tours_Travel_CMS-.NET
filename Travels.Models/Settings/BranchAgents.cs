using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Models.Base;

namespace Travels.Models.Settings
{
    public class BranchAgents : BaseEntity
    {
        public string AgentName { get; set; } =string.Empty;
        public string LiscenceNo { get; set; } =string.Empty;
        public string Email { get; set; } =string.Empty;
        public string Address { get; set; } =string.Empty;
        [ForeignKey("ContactBranchId")]
        public int ContactBranchId { get; set; }
        public ContactBranches ContactBranch { get; set; } = null!;

    }
}
