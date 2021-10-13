using System;

namespace Stregsystem.Models
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount)
            : base(user, amount)
        {
            User = user ?? throw new ArgumentNullException();
            Amount = amount;
        }


        public override bool Execute()
        {
            User.Balance += Amount;
            return true;
        }

        public override string ToString()
        {
            return $"InsertCashTransaction: User: {User}, kr. {Amount / 100}, Date: ({Date}) ID: {Id}";
        }
    }
}