namespace JWT_RoleBasedAuthorization_WebApiApp.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Position {  get; set; } = string.Empty;
    }
}
