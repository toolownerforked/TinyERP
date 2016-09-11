import {ValidationException} from "../../../common/models/exception"
export class AddPermissionModel {
    public name: string = String.empty;
    public key: string = String.empty;
    public description: string = String.empty;
    public validate(): boolean {
        let validation: ValidationException = new ValidationException();
        if (String.isNullOrWhiteSpace(this.name)) {
            validation.add("security.addPermission.nameIsRequired");
        }
        if (String.isNullOrWhiteSpace(this.key)) {
            validation.add("security.addPermission.keyIsRequired");
        }
        if (this.name.length > 255) {
            validation.add("security.addPermission.nameIsLessThan255Characters");
        }
        if (this.key.length > 255) {
            validation.add("security.addPermission.keyIsLessThan255Characters");
        }
        let namePattern: any = /^[a-zA-Z-]+$/;
        if (!namePattern.test(this.name)) {
            validation.add("security.addPermission.nameContainsOnlyLettersAndHyphenMinusCharacter");
        }
        let keyPattern: any = /^[a-zA-Z-]+$/;
        if (!keyPattern.test(this.key)) {
            validation.add("security.addPermission.keyContainsOnlyLettersAndHyphenMinusCharacter");
        }
        validation.throwIfHasError();
        return !validation.hasError();
    }
}