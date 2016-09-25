export const validationConfig = {
    permission: {
        addOrUpdatePermission: {
            name: {
                length: 255,
                pattern: "^[a-zA-Z-]+$"
            },
            key: {
                length: 255,
                pattern: "^[a-zA-Z-]+$"
            }
        }
    }
};
