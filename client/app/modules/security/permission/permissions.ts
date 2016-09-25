import {Component} from "angular2/core";
import {BasePage} from "../../../common/models/ui";
import {PageActions, Grid} from "../../../common/directive";
import {PermissionsModel} from "./permissionsModel";
import {PageAction} from "../../../common/models/ui";
import permissionService from "../_share/services/permissionService";
import {Router} from "angular2/Router";
import {routeConfig} from "../_share/config/route";
@Component({
    templateUrl: "app/modules/security/permission/permissions.html",
    directives: [PageActions, Grid]
})
export class Permissions extends BasePage {
    public model: PermissionsModel;
    public router: Router;

    constructor(router: Router) {
        super();
        let self: Permissions = this;
        self.router = router;
        self.model = new PermissionsModel(self.i18nHelper);
        this.model.addAction(new PageAction("btnAddPermission", "security.permissions.addPermission", () => self.onAddPermissionClicked()));
        this.loadPermissions();
    }

    private loadPermissions() {
        let self: Permissions = this;
        permissionService.getPermissions().then(function (perItems: Array<any>) {
            self.model.import(perItems);
        });
    }

    private onAddPermissionClicked() {
        this.router.navigate([routeConfig.permission.addPermission.name]);
    }

    public onPermissionDeleteClicked(perItem: any) {
        let self: Permissions = this;
        permissionService.delete(perItem.item.id).then(function () {
            self.loadPermissions();
        });
    }

    public onPermissionEditClicked(perItem: any) {
        this.router.navigate([routeConfig.permission.updatePermission.name, { id: perItem.item.id }]);
    }
} 