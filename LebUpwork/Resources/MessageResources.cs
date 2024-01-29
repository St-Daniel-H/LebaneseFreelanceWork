using LebUpwor.core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LebUpwork.Api.Resources
{
    public class MessageResources
    {
        public int MessageId { get; set; }
        public  string Text { get; set; }
        public  DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public User? Sender { get; set; }
        public User? Receiver { get; set; }

    }
}
