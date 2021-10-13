using Stregsystem.Exceptions;
using Stregsystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Stregsystem
{
    public class Stregsystem : IStregsystem
    {
        public event EventHandler<UserBalanceNotification> UserBalanceWarning;

        public List<Product> ProductsList = new List<Product>();
        public IEnumerable<Product> ActiveProducts => ProductsList.Where(Product => Product.Active);
        public List<User> UserList = new List<User>();
        public List<Transaction> TransactionList = new List<Transaction>();

        public bool LoadProductsList(string filepath)
        {
            using (var rd = new StreamReader(@filepath))
            {
                //skips the first
                rd.ReadLine();

                while (!rd.EndOfStream)
                {
                    bool SeasonProductFlag = false;
                    DateTime ProdEndDate = new DateTime(1);
                    DateTime ProdStartDate = new DateTime(1);

                    var sp = rd.ReadLine().Split(';');

                    int ProdId = Convert.ToInt32(sp[0]);
                    string ProdName = Regex.Replace(sp[1], "<.*?>", String.Empty);
                    decimal ProdPrice = Convert.ToDecimal(sp[2]);
                    bool ProdActive = (Convert.ToInt32(sp[3]) == 1) ? true : false;

                    if (sp[4] != string.Empty)
                    {                                   //removes "
                        var RegFixDate = Regex.Replace(sp[4], "\"", String.Empty);
                        ProdEndDate = DateTime.Parse(RegFixDate);
                        SeasonProductFlag = true;
                    }

                    // different way than above b/c column sp[4] is filled with "empty string" and dates
                    // and sp[5] doesnt exist until an intance of it is encountered.
                    if (sp.Length > 5)
                    {                                   //removes "
                        var RegFixDate = Regex.Replace(sp[5], "\"", String.Empty);
                        ProdStartDate = Convert.ToDateTime(RegFixDate);
                        SeasonProductFlag = true;
                    }

                    if (SeasonProductFlag == false)
                        ProductsList.Add(new Product(ProdId, ProdName, ProdPrice, ProdActive, false));
                    else if (SeasonProductFlag == true)
                        ProductsList.Add(new SeasonalProduct(ProdId, ProdName, ProdPrice, ProdActive, false, ProdStartDate, ProdEndDate));
                    else
                        Console.WriteLine("Something prevented a product from being loaded into the product list");
                }
            }

            // debug cw
            #region
            //foreach (var item in ProductsList)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            return true;
        }

        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            var InsTransaction = new InsertCashTransaction(user, amount);
            ExecuteTransaction(InsTransaction);
            return InsTransaction;
        }

        public BuyTransaction BuyProduct(int count, User user, Product product)
        {
            var BuyTrans = new BuyTransaction(count, user, product, product.Price);
            ExecuteTransaction(BuyTrans);
            return BuyTrans; 
        }

        private bool ExecuteTransaction(Transaction trans)
        {
            if (!trans.Execute())
            {
                throw new TransactionExecuteFailureException();
            }

            if (trans.User.Balance - trans.Amount  <= 5000)
            {
                UserBalanceWarning?.Invoke(this, new UserBalanceNotification(trans.User)); 
            }
            //logger
            TransactionList.Add(trans);

            using (var lgr = new StreamWriter("..\\..\\..\\Data\\" + "LogFile.txt", true))
            {
                lgr.WriteLine(trans);
                
            }

            return true;
        }

        public Product GetActiveProductByID(int id)
        {
            Product prod = ActiveProducts.SingleOrDefault(p => p.Id == id);

            if (prod == null)
                throw new ProductNotFoundException();

            return prod;
        }

        public Product GetProductByID(int id)
        {
            Product prod = ProductsList.SingleOrDefault(p => p.Id == id);

            if (prod == null)
                throw new ProductNotFoundException();

            return prod;
        }

        public List<Transaction> GetTransactions(User user, int count)
        {
            if (user.Username == null)
                throw new UsernameNotFoundException();

            //create with "query"
            IEnumerable<Transaction> UserTransList = TransactionList.Where(x => x.User.Equals(user)).Reverse().Take(count);
            //transform to list (sortable)
            List<Transaction> UserTransListSorted = UserTransList.ToList();
            //sort
            UserTransListSorted.Sort();

            return UserTransListSorted;
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                var prod = UserList.Single(u => u.Username == username);
                return prod;
            }
            catch (InvalidOperationException)
            {
                throw new UsernameNotFoundException();
            }
        }

        public bool LoadUserList(string filepath)
        {
            List<User> ul = new List<User>();

            using (var rd = new StreamReader(@filepath))
            {
                //skips the first
                rd.ReadLine();

                while (!rd.EndOfStream)
                {
                    var sp = rd.ReadLine().Split(',');

                    int Id = Convert.ToInt32(sp[0]);
                    string Firstname = sp[1];
                    string Lastname = sp[2];
                    string Username = sp[3];
                    decimal Balance = Convert.ToDecimal(sp[4]);
                    string Email = sp[5];

                    ul.Add(new User(Id, Firstname, Lastname, Username, Email, Balance));
                }
            }

            UserList = ul;

            // debug cw
            #region
            //foreach (var item in pl)
            //{
            //    var id = item.Id.ToString();
            //    Console.WriteLine(id + "  " + item.Firstname + "  " + item.Lastname + "  " + item.Username + "  " + item.Balance + "  " + item.Email);
            //}
            #endregion

            return true;
        }

         
    }
}