using App.Common;
using App.Common.Data;
using App.Common.DI;
using App.Context;
using App.Entity.Security;
using App.Repository.Security;
using App.Service.Security;
using System.Collections.Generic;

namespace App.Service.Impl.Security
{
    public class PermissionService : IPermissionService
    {
        public void Create(Permission request)
        {
            using (IUnitOfWork uow = new UnitOfWork(new AppDbContext(IOMode.Write)))
            {
                IPermissionRepository permissionRepo = IoC.Container.Resolve<IPermissionRepository>(uow);
                Permission permission = new Permission(request.Name, request.Key, request.Description);
                permissionRepo.Add(permission);
                uow.Commit();
            }
        }

        public IList<PermissionListItem> GetPermissions()
        {
            IPermissionRepository permissionRepo = IoC.Container.Resolve<IPermissionRepository>();
            return permissionRepo.GetItems<PermissionListItem>();
        }
    }
}
