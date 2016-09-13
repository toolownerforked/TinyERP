import configHelper from "../../../../common/helpers/configHelper";
let permissionService = {
    getPermissions: getPermissions,
    delete: remove,
    create: create,
    getPermission: getPermission,
    update: update
};
export default permissionService;
function getPermissions() {
    var url = String.format("{0}/permissions", configHelper.getAppConfig().api.baseUrl);
    var connector = window.ioc.resolve("IConnector");
    return connector.get(url);
}
function remove(id: any) {
    var url = String.format("{0}/permissions/{1}", configHelper.getAppConfig().api.baseUrl, id);
    var connector = window.ioc.resolve("IConnector");
    return connector.delete(url);
}
function create(item: any) {
    var url = String.format("{0}/permissions", configHelper.getAppConfig().api.baseUrl);
    var connector = window.ioc.resolve("IConnector");
    return connector.post(url, item);
}
function getPermission(id: any) {
    var url = String.format("{0}/permissions/{1}", configHelper.getAppConfig().api.baseUrl, id);
    var connector = window.ioc.resolve("IConnector");
    return connector.get(url);
}
function update(id: any, item: any) {
    var url = String.format("{0}/permissions/{1}", configHelper.getAppConfig().api.baseUrl, id);
    var connector = window.ioc.resolve("IConnector");
    return connector.put(url, item);
}