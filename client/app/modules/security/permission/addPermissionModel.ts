import {ValidationException} from "../../../common/models/exception";
import {validationConfig} from "../_share/config/validation";
import regexHelper from "../../../common/helpers/regexHelper";
export class AddPermissionModel {
    public name: string = String.empty;
    public key: string = String.empty;
    public description: string = String.empty;
    public validate(): boolean {
        let validation: ValidationException = new ValidationException();
        if (String.isNullOrWhiteSpace(this.name)) {
            validation.add("security.addPermission.nameIsRequired");
        }
        if (this.name.length > validationConfig.permission.addPermission.key.length) {
            validation.add("security.addPermission.nameIsTooLong");
        }
        if (!regexHelper.isMatch(validationConfig.permission.addPermission.name.pattern, this.name)) {
            validation.add("security.addPermission.nameMatchesThisPattern");
        }
        if (String.isNullOrWhiteSpace(this.key)) {
            validation.add("security.addPermission.keyIsRequired");
        }
        if (this.key.length > validationConfig.permission.addPermission.key.length) {
            validation.add("security.addPermission.keyIsTooLong");
        }
        if (!regexHelper.isMatch(validationConfig.permission.addPermission.key.pattern, this.key)) {
            validation.add("security.addPermission.keyMatchesThisPattern");
        }
        validation.throwIfHasError();
        return !validation.hasError();
    }
}