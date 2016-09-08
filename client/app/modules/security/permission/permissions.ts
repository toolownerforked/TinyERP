import {Component} from "angular2/core";
import {BasePage} from "../../../common/models/ui";
import {PageActions, Grid} from "../../../common/directive"
import {PermissionsModel} from "./permissionsModel";
import {PageAction} from "../../../common/models/ui"
import permissionService from "../_share/services/permissionService";
@Component({
    templateUrl: "app/modules/security/permission/permissions.html",
    directives: [PageActions, Grid]
})
export class Permissions extends BasePage {
    public model: PermissionsModel;
    constructor() {
        super();
        let self: Permissions = this;
        self.model = new PermissionsModel(self.i18nHelper);
        this.model.addAction(new PageAction("btnAddPermission", "security.permissions.addPermission", () => self.onAddPermissionClicked()));
        this.loadPermissions();
    }
    private loadPermissions() {
        let self: Permissions = this;
        permissionService.getPermissions().then(function (perItems: Array<any>) {
            self.model.import(perItems);
        })
    }
    private onAddPermissionClicked() {
        console.log("Add Permission");
    }
} 