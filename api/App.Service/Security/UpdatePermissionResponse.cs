using App.Common.Data;
using App.Common.Mapping;
using App.Entity.Security;

namespace App.Service.Security
{
    public class UpdatePermissionResponse : BaseContent, IMappedFrom<Permission>
    {
    }
}