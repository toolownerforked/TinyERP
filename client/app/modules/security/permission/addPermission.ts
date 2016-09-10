import {Component} from "angular2/core";
import {BasePage} from "../../../common/models/ui";
import {AddPermissionModel} from "./addPermissionModel";
import {Router} from "angular2/Router";
import permissionService from "../_share/services/permissionService";
import {router} from "../_share/config/route";
import {ValidationDirective} from "../../../common/directive"
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
            self.router.navigate([router.permission.permissions.name]);
        });
    }
    public onCancelClicked() {
        this.router.navigate([router.permission.permissions.name]);
    }
}