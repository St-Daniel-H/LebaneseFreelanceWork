﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LebUpwor.core.Models
{
    public class NewJob
    {
        public  int JobId { get; set; }
        public  int UserId { get; set; }
        public  DateTime? Date { get; set; }
        public NewJob()
        {
          //  Date = DateTime.Now;
        }
        public Job? Job { get; set; }
        public User? User { get; set; }
    }
}
