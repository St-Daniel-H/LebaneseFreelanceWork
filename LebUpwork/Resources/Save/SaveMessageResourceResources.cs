using LebUpwor.core.Models;

namespace LebUpwork.Api.Resources.Save
{
    public class SaveMessageResource
    {
        public string Text { get; set; }
        public User? Sender { get; set; }
        public User? Receiver { get; set; }
    }
}
