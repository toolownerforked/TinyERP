import {Component} from "angular2/core";
import {ValidationDirective} from "../../../common/directive";
import {BasePage} from "../../../common/models/ui";
import {AddOrUpdatePermissionModel} from "./addOrUpdatePermissionModel";
import {Router, RouteParams} from "angular2/Router";
import permissionService from "../_share/services/permissionService";
import {routeConfig} from "../_share/config/route";
@Component({
    templateUrl: "app/modules/security/permission/addOrUpdatePermission.html",
    directives: [ValidationDirective]
})
export class AddOrUpdatePermission extends BasePage {
    public model: AddOrUpdatePermissionModel = new AddOrUpdatePermissionModel();
    public router: Router;
    public selectedPerItemId: any;
    constructor(router: Router, routeParams: RouteParams) {
        super();
        this.router = router;
        if (routeParams.get("id")) {
            this.selectedPerItemId = routeParams.get("id");
            let self: AddOrUpdatePermission = this;
            permissionService.getPermission(this.selectedPerItemId).then(function (perItem: any) {
                self.model.name = perItem.name;
                self.model.key = perItem.key;
                self.model.description = perItem.description;
            });
        }
    }
    public onSaveClicked() {
        let self: AddOrUpdatePermission = this;
        if (!self.model.validate()) { return; }
        permissionService.create(this.model).then(function () {
            self.router.navigate([routeConfig.permission.permissions.name]);
        });
    }
    public onCancelClicked() {
        this.router.navigate([routeConfig.permission.permissions.name]);
    }
    public onEditClicked() {
        let self: AddOrUpdatePermission = this;
        if (!self.model.validate()) { return; }
        permissionService.update(this.selectedPerItemId, this.model).then(function () {
            self.router.navigate([routeConfig.permission.permissions.name]);
        });
    }
}