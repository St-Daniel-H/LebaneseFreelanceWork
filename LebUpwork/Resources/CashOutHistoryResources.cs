using LebUpwor.core.Models;

namespace LebUpwork.Api.Resources
{
    public class CashOutHistoryResources
    {
        public  int? CashOutHistoryId { get; set; }
        public double? Amount { get; set; }
        public DateTime? Date { get; set; }
        public  User? User { get; set; }
    }
}
