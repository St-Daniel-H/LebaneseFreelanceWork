﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LebUpwor.core.Models
{
    public class Message
    {
        [Key]
        public required int MessageId { get; set; }
        public required int SenderId { get; set; }
        public required int ReceiverId { get; set; }
        [StringLength(1000)]
        public required string Text { get; set; }
        public required DateTime Date { get; set; }

        public bool IsRead { get; set; }

        [ForeignKey("SenderId")]
        public  User Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public  User Receiver { get; set; }
        public Message()
        {
            Date = DateTime.Now;
            IsRead = false;
        }
    }
}
