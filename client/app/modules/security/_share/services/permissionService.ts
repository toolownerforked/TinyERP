import configHelper from "../../../../common/helpers/configHelper";
let permissionService = {
    getPermissions: getPermissions
};
export default permissionService;
function getPermissions() {
    var url = String.format("{0}/permissions", configHelper.getAppConfig().api.baseUrl);
    var connector = window.ioc.resolve("IConnector");
    return connector.get(url);
}