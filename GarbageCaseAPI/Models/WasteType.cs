using System.ComponentModel.DataAnnotations;

namespace GarbageCaseAPI.Models
{
    public class WasteType
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string? WasteTypeName { get; set; }
        [StringLength(250)]
        public string? WasteProperty { get; set; }
    }
}
