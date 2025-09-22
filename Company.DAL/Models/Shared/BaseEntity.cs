namespace Company.DAL.Models.Shared
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
