export const validationConfig = {
    permission: {
        addPermission: {
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
