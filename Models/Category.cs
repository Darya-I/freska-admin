using System.ComponentModel.DataAnnotations;

namespace Admin_microservice_v2.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [MaxLength(50)]
        public string CategoryName { get; set; }

        public List<Subcategory>? Subcategories { get; set; }
    }
}
