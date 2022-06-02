using System.ComponentModel.DataAnnotations;

namespace GarbageCaseAPI.Models
{
    public class Store
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;
    }
}
