using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firstcod.FC.Provider.Models
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }
        public string Account { get; set; }
        public string TransactionHash { get; set; }

        [Column(TypeName = "decimal(18,8)")]
        public decimal Amount { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public long StateId { get; set; }
    }
}
