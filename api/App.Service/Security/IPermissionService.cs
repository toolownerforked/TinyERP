using App.Entity.Security;
using System.Collections.Generic;

namespace App.Service.Security
{
    public interface IPermissionService
    {
        IList<PermissionListItem> GetPermissions();
        void Create(Permission request);
    }
}
