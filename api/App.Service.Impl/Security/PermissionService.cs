using App.Common;
using App.Common.Data;
using App.Common.DI;
using App.Context;
using App.Entity.Security;
using App.Repository.Security;
using App.Service.Security;
using System.Collections.Generic;
using System;
using App.Common.Validation;

namespace App.Service.Impl.Security
{
    public class PermissionService : IPermissionService
    {
        public Permission Create(Permission request)
        {
            Permission permission;
            using (IUnitOfWork uow = new UnitOfWork(new AppDbContext(IOMode.Write)))
            {
                IPermissionRepository permissionRepo = IoC.Container.Resolve<IPermissionRepository>(uow);
                permission = new Permission(request.Name, request.Key, request.Description);
                permissionRepo.Add(permission);
                uow.Commit();
            }
            return permission;
        }

        public void DeletePermission(string itemId)
        {
            ValiateForDeletion(itemId);
            using (IUnitOfWork uow = new UnitOfWork(new AppDbContext(IOMode.Write)))
            {
                IPermissionRepository permissionRepo = IoC.Container.Resolve<IPermissionRepository>(uow);
                permissionRepo.Delete(itemId);
                uow.Commit();
            }
        }

        private void ValiateForDeletion(string itemId)
        {
            Guid id;
            if (!Guid.TryParse(itemId, out id))
            {
                throw new ValidationException("security.deletePermission.invalidId");
            }
        }

        public IList<PermissionListItem> GetPermissions()
        {
            IPermissionRepository permissionRepo = IoC.Container.Resolve<IPermissionRepository>();
            return permissionRepo.GetItems<PermissionListItem>();
        }
    }
}
