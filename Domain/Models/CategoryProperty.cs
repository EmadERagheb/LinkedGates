namespace Domain.Models
{
   public class CategoryProperty : BaseDomainModel
    {
        public int CategoryId { get; set; }

        public int PropertyId { get; set; }

        public Category Category { get; set; } = null!;
        public Property Property { get; set; } = null!;
    }
}
