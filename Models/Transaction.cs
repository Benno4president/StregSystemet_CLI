using System;
using System.Diagnostics.CodeAnalysis;

namespace Stregsystem.Models
{
    public class Transaction : IComparable<Transaction>
    {

        public Guid Id { get; private set; } 
        public User User { get; set; }
        public DateTime Date { get; private set; }
        public decimal Amount { get; set; }
        
        public virtual bool Execute()
        {
            throw new NotImplementedException(); // if this is called directly, something is implemented wrong..
        }

        //contructer
        public Transaction(User user, decimal amount)
        {
            Id = Guid.NewGuid();
            User = user ?? throw new ArgumentNullException();
            Date = DateTime.Now;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Id} {User} {Amount/100} ({Date})";
        }

        // allows sort (icomp...)
        public int CompareTo([AllowNull] Transaction other)
        {
            return (other.Date < this.Date ? -1 : (other.Date > this.Date) ? 1 : 0);
        }

    }
}