import {ValidationException} from "../../../common/models/exception";
import {validationConfig} from "../_share/config/validation";
import regexHelper from "../../../common/helpers/regexHelper";
export class AddOrUpdatePermissionModel {
    public name: string = String.empty;
    public key: string = String.empty;
    public description: string = String.empty;
    public validate(): boolean {
        let validation: ValidationException = new ValidationException();
        if (String.isNullOrWhiteSpace(this.name)) {
            validation.add("security.addOrUpdatePermission.nameIsRequired");
        }
        if (this.name.length > validationConfig.permission.addOrUpdatePermission.key.length) {
            validation.add("security.addOrUpdatePermission.nameIsTooLong");
        }
        if (!regexHelper.isMatch(validationConfig.permission.addOrUpdatePermission.name.pattern, this.name)) {
            validation.add("security.addOrUpdatePermission.nameMatchesThisPattern");
        }
        if (String.isNullOrWhiteSpace(this.key)) {
            validation.add("security.addOrUpdatePermission.keyIsRequired");
        }
        if (this.key.length > validationConfig.permission.addOrUpdatePermission.key.length) {
            validation.add("security.addOrUpdatePermission.keyIsTooLong");
        }
        if (!regexHelper.isMatch(validationConfig.permission.addOrUpdatePermission.key.pattern, this.key)) {
            validation.add("security.addOrUpdatePermission.keyMatchesThisPattern");
        }
        validation.throwIfHasError();
        return !validation.hasError();
    }
}