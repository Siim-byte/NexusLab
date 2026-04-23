using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexus.Core.Domain;
using Nexus.Core.Dto;
using Nexus.Nexus.Core.Domain;

namespace Nexus.Core.SeviceInterfrace
{
    public interface ICommentsServices
    {
        Task<Comment> Create(CommentsDTO dto);
        Task<Comment> DetailsAsync(Guid id);
        Task<Comment> Delete(Guid id);

    }
}
