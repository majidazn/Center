using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Center.Common.Dtos
{
    public class UserRoleDto
    {
        public int ApplicationId { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; } = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public bool IsUserInTheRole { get; set; }
        public int UserId { get; set; }
    }

}
