using LebUpwor.core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LebUpwork.Api.Resources
{
    public class TokenHistoryResources
    {
        public int TokenHistoryId { get; set; }
        public required double AmountSent { get; set; }
        public required DateTime Date { get; set; }
        public required Job Job { get; set; }
        public   User? Sender { get; set; }
        public   User? Receiver { get; set; }

    }
}
