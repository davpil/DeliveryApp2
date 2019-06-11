using DeliveryApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models
{
    public class TransactionEntity : Entity<int>
    {
        public DateTime Date { get; set; }
        public int BillID { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public TransactionType BillType { get; set; } = TransactionType.Cash;
    }
}
