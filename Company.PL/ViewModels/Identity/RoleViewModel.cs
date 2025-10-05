namespace Company.PL.ViewModels.Identity
{
    public class RoleViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public RoleViewModel()
        {
            Id =Guid.NewGuid().ToString();
        }
    }
}
