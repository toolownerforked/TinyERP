using App.Entity.Security;
using System.Collections.Generic;

namespace App.Service.Security
{
    public interface IPermissionService
    {
        IList<PermissionListItem> GetPermissions();
        Permission Create(CreatePermissionRequest request);
        void DeletePermission(string itemId);
    }
}
