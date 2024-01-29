using LebUpwor.core.Models;

namespace LebUpwork.Api.Resources.Save
{
    public class SaveTokenHistoryResources
    {
        public required double AmountSent { get; set; }
        public required Job Job { get; set; }
        public User? Sender { get; set; }
        public User? Receiver { get; set; }
    }
}
