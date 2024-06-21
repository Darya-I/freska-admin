using System.ComponentModel.DataAnnotations;

namespace Admin_microservice_v2.Models
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        [MaxLength(30)]
        public string GenderValue { get; set; }

        public List<Item>? Items { get; set; }
    }
}
