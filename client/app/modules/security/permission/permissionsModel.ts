import {EventManager} from "../../../common/eventManager";
export class PermissionsModel {
    public actions: Array<any> = [];
    public options: any = {};
    public eventKey = "permissions_ondataloaded";

    constructor(resourceHelper: any) {
        this.options = {
            data: [],
            columns: [
                { field: "name", title: resourceHelper.resolve("security.permissions.grid.name"), index: 0 },
                { field: "key", title: resourceHelper.resolve("security.permissions.grid.key"), index: 1 },
                { field: "description", title: resourceHelper.resolve("security.permissions.grid.description"), index: 2 }
            ],
            enableDelete: true,
            enableEdit: true
        };
    }

    public addAction(action: any): void {
        this.actions.push(action);
    }

    public import(items: Array<any>) {
        let eventManager = window.ioc.resolve("IEventManager");
        eventManager.publish(this.eventKey, items);
    }

}