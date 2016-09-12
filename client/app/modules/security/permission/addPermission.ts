import {Component} from "angular2/core";
import {ValidationDirective} from "../../../common/directive";
import {BasePage} from "../../../common/models/ui";
import {AddPermissionModel} from "./addPermissionModel";
import {Router} from "angular2/Router";
import permissionService from "../_share/services/permissionService";
import {routeConfig} from "../_share/config/route";
@Component({
    templateUrl: "app/modules/security/permission/addPermission.html",
    directives: [ValidationDirective]
})
export class AddPermission extends BasePage {
    public model: AddPermissionModel = new AddPermissionModel();
    public router: Router;
    constructor(router: Router) {
        super();
        this.router = router;
    }
    public onSaveClicked() {
        let self: AddPermission = this;
        if (!self.model.validate()) { return; }
        permissionService.create(this.model).then(function () {
            self.router.navigate([routeConfig.permission.permissions.name]);
        });
    }
    public onCancelClicked() {
        this.router.navigate([routeConfig.permission.permissions.name]);
    }

}