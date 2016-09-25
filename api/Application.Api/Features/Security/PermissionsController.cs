using App.Common.DI;
using App.Common.Http;
using App.Common.Validation;
using App.Service.Security;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace App.Api.Features.Security
{
    [RoutePrefix("api/permissions")]
    public class PermissionsController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IResponseData<IList<PermissionListItem>> GetPermissions()
        {
            IResponseData<IList<PermissionListItem>> response = new ResponseData<IList<PermissionListItem>>();
            try
            {
                IPermissionService permissionService = IoC.Container.Resolve<IPermissionService>();
                IList<PermissionListItem> items = permissionService.GetPermissions();
                response.SetData(items);
            }
            catch (ValidationException ex)
            {
                response.SetStatus(HttpStatusCode.PreconditionFailed);
                response.SetErrors(ex.Errors);
            }
            return response;
        }

        [Route("{itemId}")]
        [HttpGet]
        public IResponseData<GetPermissionResponse> GetById([FromUri]string itemId)
        {
            IResponseData<GetPermissionResponse> response = new ResponseData<GetPermissionResponse>();
            try
            {
                IPermissionService perService = IoC.Container.Resolve<IPermissionService>();
                GetPermissionResponse item = perService.GetById(itemId);
                response.SetData(item);
            }
            catch (ValidationException ex)
            {
                response.SetStatus(HttpStatusCode.PreconditionFailed);
                response.SetErrors(ex.Errors);
            }
            return response;
        }

        [Route("{itemId}")]
        [HttpDelete]
        public IResponseData<string> DeletePermission([FromUri]string itemId)
        {
            IResponseData<string> response = new ResponseData<string>();
            try
            {
                IPermissionService perservice = IoC.Container.Resolve<IPermissionService>();
                perservice.DeletePermission(itemId);
            }
            catch (ValidationException ex)
            {

                response.SetStatus(System.Net.HttpStatusCode.PreconditionFailed);
                response.SetErrors(ex.Errors);
            }
            return response;
        }

        [Route("")]
        [HttpPost]
        public IResponseData<string> CreatePermission([FromBody]CreatePermissionRequest request)
        {
            IResponseData<string> response = new ResponseData<string>();
            try
            {
                IPermissionService perService = IoC.Container.Resolve<IPermissionService>();
                perService.Create(request);
            }
            catch (ValidationException ex)
            {
                response.SetStatus(HttpStatusCode.PreconditionFailed);
                response.SetErrors(ex.Errors);
            }
            return response;
        }

        [Route("{itemId}")]
        [HttpPut]
        public IResponseData<GetPermissionResponse> UpdatePermission([FromUri]string itemId, [FromBody]UpdatePermissionRequest request)
        {
            IResponseData<GetPermissionResponse> response = new ResponseData<GetPermissionResponse>();
            try
            {
                IPermissionService perService = IoC.Container.Resolve<IPermissionService>();
                perService.Update(itemId, request);
                GetPermissionResponse item = perService.GetById(itemId);
                response.SetData(item);
            }
            catch (ValidationException ex)
            {
                response.SetStatus(HttpStatusCode.PreconditionFailed);
                response.SetErrors(ex.Errors);
            }
            return response;
        }
    }
}
