using App.Entity.Security;
using System.Collections.Generic;

namespace App.Service.Security
{
    public interface IPermissionService
    {
        IList<PermissionListItem> GetPermissions();
        Permission Create(Permission request);
        void DeletePermission(string itemId);
    }
}
