using App.Common;
using App.Common.DI;
using App.Common.Tasks;
using App.Service.Security;
using System.Web;

namespace App.Api.Features.Share.Tasks.Data
{
    public class CreatePermissionsTask : BaseTask<TaskArgument<HttpApplication>>, IApplicationReadyTask<TaskArgument<HttpApplication>>
    {
        public CreatePermissionsTask() : base(ApplicationType.All)
        {

        }

        public override void Execute(TaskArgument<HttpApplication> context)
        {
            IPermissionService permissionService = IoC.Container.Resolve<IPermissionService>();
            permissionService.Create(new Entity.Security.Permission("p01", "Add New File", "Add a file to folder"));
        }
    }
}