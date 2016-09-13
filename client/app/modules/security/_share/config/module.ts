import {IModule, Module, MenuItem} from "../../../../common/models/layout";
import {AuthenticationMode} from "../../../../common/enum";
import {Permissions} from "./../../permission/permissions";
import {AddOrUpdatePermission} from "../../permission/addOrUpdatePermission";
import {routeConfig} from "./route";
let security: IModule = createModule();
export default security;
function createModule() {
    let module = new Module("app/modules/security", "security");
    module.menus.push(
        new MenuItem(
            "Security", "/Permissions", "fa fa-edit",
            new MenuItem("Permissions", "/Permissions", "")
        )
    );
    module.addRoutes([
        { path: routeConfig.permission.permissions.path, name: routeConfig.permission.permissions.name, component: Permissions, data: { authentication: AuthenticationMode.Require } },
        { path: routeConfig.permission.addPermission.path, name: routeConfig.permission.addPermission.name, component: AddOrUpdatePermission, data: { authentication: AuthenticationMode.Require } },
        { path: routeConfig.permission.updatePermission.path, name: routeConfig.permission.updatePermission.name, component: AddOrUpdatePermission, data: { authentication: AuthenticationMode.Require } }
    ]);
    return module;
}