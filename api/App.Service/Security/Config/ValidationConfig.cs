namespace App.Service.Security.Config
{
    public static class ValidationConfig
    {
        public const int nameLength = 255;
        public const string namePattern = "^[a-zA-Z-]+$";
        public const int keyLength = 255;
        public const string keyPattern = "^[a-zA-Z-]+$";
    }
}
