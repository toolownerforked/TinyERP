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
        public void Update(string itemId, UpdatePermissionRequest request)
        {
            ValiateForUpdate(itemId, request);
            using (IUnitOfWork uow = new UnitOfWork(new AppDbContext(IOMode.Write)))
            {
                IPermissionRepository permissionRepo = IoC.Container.Resolve<IPermissionRepository>(uow);
                Permission permission = permissionRepo.GetById(itemId);
                permission.Name = request.Name;
                permission.Key = request.Key;
                permission.Description = request.Description;
                permissionRepo.Update(permission);
                uow.Commit();
            }
        }

        private void ValiateForUpdate(string itemId, UpdatePermissionRequest request)
        {
            IPermissionRepository permissionRepo = IoC.Container.Resolve<IPermissionRepository>();
            Permission oldItem = permissionRepo.GetById(itemId);
            if (oldItem == null)
            {
                throw new ValidationException("security.addOrUpdatePermission.itemIsNotExist");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ValidationException("security.addOrUpdatePermission.nameIsRequired");
            }
            if (request.Name.Length > ValidationConfig.nameLength)
            {
                throw new ValidationException("security.addOrUpdatePermission.nameIsTooLong");
            }
            if (!Regex.IsMatch(request.Name, ValidationConfig.namePattern))
            {
                throw new ValidationException("security.addOrUpdatePermission.nameMatchesThisPattern");
            }
            if (oldItem.Name != request.Name)
            {
                if (permissionRepo.GetByName(request.Name) != null)
                {
                    throw new ValidationException("security.addOrUpdatePermission.nameIsTaken");
                }
            }
            if (string.IsNullOrWhiteSpace(request.Key))
            {
                throw new ValidationException("security.addOrUpdatePermission.keyIsRequired");
            }
            if (request.Key.Length > ValidationConfig.keyLength)
            {
                throw new ValidationException("security.addOrUpdatePermission.keyIsTooLong");
            }
            if (!Regex.IsMatch(request.Key, ValidationConfig.keyPattern))
            {
                throw new ValidationException("security.addOrUpdatePermission.keyMatchesThisPattern");
            }
            if (oldItem.Key != request.Key)
            {
                if (permissionRepo.GetByKey(request.Key) != null)
                {
                    throw new ValidationException("security.addOrUpdatePermission.keyIsTaken");
                }
            }

        }

        public GetPermissionResponse GetById(string itemId)
        {
            IPermissionRepository permissionRepo = IoC.Container.Resolve<IPermissionRepository>();
            return permissionRepo.GetById<GetPermissionResponse>(itemId);
        }
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
                throw new ValidationException("security.addOrUpdatePermission.nameIsRequired");
            }
            if (request.Name.Length > ValidationConfig.nameLength)
            {
                throw new ValidationException("security.addOrUpdatePermission.nameIsTooLong");
            }
            if (!Regex.IsMatch(request.Name, ValidationConfig.namePattern))
            {
                throw new ValidationException("security.addOrUpdatePermission.nameMatchesThisPattern");
            }
            if (permissionRepo.GetByName(request.Name) != null)
            {
                throw new ValidationException("security.addOrUpdatePermission.nameIsTaken");
            }
            if (string.IsNullOrWhiteSpace(request.Key))
            {
                throw new ValidationException("security.addOrUpdatePermission.keyIsRequired");
            }
            if (request.Key.Length > ValidationConfig.keyLength)
            {
                throw new ValidationException("security.addOrUpdatePermission.keyIsTooLong");
            }
            if (!Regex.IsMatch(request.Key, ValidationConfig.keyPattern))
            {
                throw new ValidationException("security.addOrUpdatePermission.keyMatchesThisPattern");
            }
            if (permissionRepo.GetByKey(request.Key) != null)
            {
                throw new ValidationException("security.addOrUpdatePermission.keyIsTaken");
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
