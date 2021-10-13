using Stregsystem.Exceptions;
using System;

namespace Stregsystem.Models
{
    public class BuyTransaction : Transaction
    {
        public Product Product { get; set; }
        public int Count { get; set; }

        public BuyTransaction(int count, User user, Product product, decimal amount)
            : base(user, amount)
        {
            Count = (count >= 1) ? count : throw new ArgumentOutOfRangeException();
            User = user ?? throw new ArgumentNullException();
            Product = product;
            Amount = amount;
        }

        public override bool Execute()
        {
            if (User.Balance - Amount * Count > 0 || Product.CanBeBoughtOnCredit == true)
            {
                User.Balance = User.Balance - Amount * Count;
                return true;
            }
            else
                throw new InsufficientCreditsException(User, Product);
        }

        public override string ToString()
        {
            return $"BuyTransaction: {Count} {Product.Name} ({Product.Price * Count / 100} kr), User: {User.Username}, Date:({Date}), ID: {Id}";
        }
    }
}