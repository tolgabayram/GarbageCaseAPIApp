using System.ComponentModel.DataAnnotations;

namespace GarbageCaseAPI.Models
{
    public class WasteRecord
    {
        public int Id { get; set; }
        [StringLength(25)]
        public string Month { get; set; } = string.Empty;

        public int StoreId { get; set; }
        public Store? StoreFields { get; set; }
        public int WasteTypeId { get; set; }
        public WasteType? WasteTypeFields { get; set; }       

        public decimal Quantity { get; set; }
        [StringLength(10)]
        public string QuantityType { get; set; } = string.Empty;
        [StringLength(250)]
        public string ReceivingCompany { get; set; } = string.Empty;

        public DateTime WasteDate { get; set; }
        [StringLength(500)]
        public string WasteExplanation { get; set; } = string.Empty;
    }
}
