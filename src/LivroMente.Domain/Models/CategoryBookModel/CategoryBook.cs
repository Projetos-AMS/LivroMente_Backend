namespace LivroMente.Domain.Models.CategoryBookModel
{
    public class CategoryBook
    {
        public Guid Id { get; private set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}