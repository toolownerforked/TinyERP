namespace App.Service.Security
{
    public class CreatePermissionRequest
    {
        public CreatePermissionRequest(string name, string key, string description)
        {
            this.Name = name;
            this.Key = key;
            this.Description = description;
        }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
    }
}