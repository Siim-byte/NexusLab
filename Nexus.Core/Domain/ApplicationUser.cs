using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Core.Domain
{
    public class ApplicationUser
    {
        public List<Guid>? CommentIDs { get; set; }
        public string DisplayName { get; set; }
        public bool ProfileType { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
    }
}
