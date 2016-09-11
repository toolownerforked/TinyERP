import {IModule, Module, MenuItem} from "../../../../common/models/layout";
import {AuthenticationMode} from "../../../../common/enum";
import {Permissions} from "./../../permission/permissions";
import {AddPermission} from "../../permission/addPermission";
import {router} from "./route";
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
        { path: router.permission.permissions.path, name: router.permission.permissions.name, component: Permissions, data: { authentication: AuthenticationMode.Require } },
        { path: router.permission.addPermission.path, name: router.permission.addPermission.name, component: AddPermission, data: { authentication: AuthenticationMode.Require } }
    ]);
    return module;
}