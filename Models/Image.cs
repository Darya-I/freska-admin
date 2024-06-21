using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_microservice_v2.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")] // Указываем на свойство, связанное с внешним ключом
        public Item? Item { get; set; }

        [MaxLength(255)]
        public string? ImageUrl { get; set; }
    }
}
