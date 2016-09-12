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
using System.Text.RegularExpressions;
using App.Service.Security.Config;

namespace App.Service.Impl.Security
{
    public class PermissionService : IPermissionService
    {
        public Permission Create(CreatePermissionRequest request)
        {
            ValiateForCreation(request);
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

        private void ValiateForCreation(CreatePermissionRequest request)
        {
            IPermissionRepository permissionRepo = IoC.Container.Resolve<IPermissionRepository>();
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ValidationException("security.addPermission.nameIsRequired");
            }
            if (request.Name.Length > ValidationConfig.nameLength)
            {
                throw new ValidationException("security.addPermission.nameIsTooLong");
            }
            if (!Regex.IsMatch(request.Name, ValidationConfig.namePattern))
            {
                throw new ValidationException("security.addPermission.nameMatchesThisPattern");
            }
            if (permissionRepo.GetByName(request.Name) != null)
            {
                throw new ValidationException("security.addPermission.nameIsUnique");
            }
            if (string.IsNullOrWhiteSpace(request.Key))
            {
                throw new ValidationException("security.addPermission.keyIsRequired");
            }
            if (request.Key.Length > ValidationConfig.keyLength)
            {
                throw new ValidationException("security.addPermission.keyIsTooLong");
            }
            if (!Regex.IsMatch(request.Key, ValidationConfig.keyPattern))
            {
                throw new ValidationException("security.addPermission.keyMatchesThisPattern");
            }
            if (permissionRepo.GetByKey(request.Key) != null)
            {
                throw new ValidationException("security.addPermission.keyIsUnique");
            }
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
