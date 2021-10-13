using Stregsystem.Models;
using System;
using System.Collections.Generic;

namespace Stregsystem
{
    public interface IStregsystem
    {
        IEnumerable<Product> ActiveProducts { get; }
        bool LoadProductsList(string filepath);
        bool LoadUserList(string filepath);
        InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
        BuyTransaction BuyProduct(int count, User user, Product product);
        Product GetActiveProductByID(int id);
        Product GetProductByID(int id);
        List<Transaction> GetTransactions(User user, int count);
        User GetUserByUsername(string username);

        event EventHandler<UserBalanceNotification> UserBalanceWarning;
    }
}