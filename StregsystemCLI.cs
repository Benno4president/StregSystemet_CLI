using Stregsystem.Models;
using System;

namespace Stregsystem
{
    public class StregsystemCLI : IStregsystemUI
    {
        public event EventHandler<string> CommandEntered;

        //OBS private
        private IStregsystem Sts;

        public StregsystemCLI(IStregsystem stregsystem)
        {
            Sts = stregsystem;
            Sts.UserBalanceWarning += Stregsystem_UserBalanceWarning;
        }

        public void Start()
        {
            Console.Clear();

            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            Console.ForegroundColor = colors[12];

            while (true)
            {
                Console.Clear();
                Banner();
                Console.WriteLine("");

                foreach (var item in Sts.ActiveProducts)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine(" ___________ ");
                Console.WriteLine("| Quick Buy:|");
                string input = Console.ReadLine();

                Console.Clear();

                CommandEntered?.Invoke(this, input);
            }
        }

        public void Close()
        {
            Console.Clear();
            Console.ResetColor();
            Environment.Exit(0);
        }

        // event
        private void Stregsystem_UserBalanceWarning(object sender, UserBalanceNotification e)
        {
            Console.WriteLine("### Your balance is Low! ###");
            Console.WriteLine("");
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"The command; {adminCommand}");
            Console.WriteLine("is not recognized.");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Press ENTER to go back");
            Console.WriteLine("<--");
            Console.ReadKey();
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine("An error was encountered.");
            Console.WriteLine(errorString);
            Console.WriteLine("");
            Console.WriteLine("Make sure to use the correct format,");
            Console.WriteLine("[Username] [Product Id]");
            Console.WriteLine("or");
            Console.WriteLine("[Username] [Amount] [Product Id]");
            Console.WriteLine("for multi purchase");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Press ENTER to go back");
            Console.WriteLine("<--");
            Console.ReadKey();
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"Unable to purchase: {product.Name}\n" +
                $"for user: {user.Username}");
            Console.WriteLine(" _______________________");
            Console.WriteLine("   Insufficient Credit   ");
            Console.WriteLine($" Product(s) price: {product.Price / 100} kr");
            Console.WriteLine($" User balance:  {user.Balance / 100} kr");
            Console.WriteLine("");
            Console.WriteLine(" Press ENTER to go back");
            Console.WriteLine("<--");
            Console.ReadKey();
        }

        public void DisplayInsufficientCash(int count, User user, Product product)
        {
            Console.WriteLine($"Unable to purchase: {count} {product.Name}\n" +
                $"for user: {user.Username}");
            Console.WriteLine(" _______________________");
            Console.WriteLine("   Insufficient Credit   ");
            Console.WriteLine($" Products price: {(product.Price * count) / 100} kr");
            Console.WriteLine($" User balance:  {user.Balance / 100} kr");
            Console.WriteLine("");
            Console.WriteLine(" Press ENTER to go back");
            Console.WriteLine("<--");
            Console.ReadKey();
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"The requested product ID; {product}");
            Console.WriteLine("could not be found");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Press ENTER to go back");
            Console.WriteLine("<--");
            Console.ReadKey();
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"The command: {command}");
            Console.WriteLine("does not match the format");
            Console.WriteLine("");
            Console.WriteLine("The Format is:");
            Console.WriteLine("[Username] [Product Id]");
            Console.WriteLine("or");
            Console.WriteLine("[Username] [Amount] [Product Id]");
            Console.WriteLine("for multi purchase");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Press ENTER to go back");
            Console.WriteLine("<--");
            Console.ReadKey();
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine("Transaction complete");
            Console.WriteLine($"User: {transaction.User.Username}");
            Console.WriteLine($"bought: {transaction.Product.Name}");
            Console.WriteLine($"for {transaction.Product.Price / 100} kr.");
            Console.WriteLine("");
            Console.WriteLine($"New balance: {transaction.User.Balance / 100} kr");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Press ENTER to go back");
            Console.WriteLine("<--");
            Console.ReadKey();
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine("Transaction complete");
            Console.WriteLine($"User: {transaction.User.Username}");
            Console.WriteLine($"bought: {count} {transaction.Product.Name}");
            Console.WriteLine($"for {transaction.Product.Price * count / 100} kr. ({transaction.Product.Price / 100}/each)");
            Console.WriteLine("");
            Console.WriteLine($"New balance: {transaction.User.Balance / 100} kr");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Press ENTER to go back");
            Console.WriteLine("<--");
            Console.ReadKey();
        }

        public void DisplayUserInfo(User user)
        {
            var TransactionList = Sts.GetTransactions(user, 10);

            Console.WriteLine($"{user} Balance: {user.Balance / 100} kr.");
            Console.WriteLine("");

            if (user.Balance / 100 < 50)
            {
                Console.WriteLine("Your balance is low!");
                Console.WriteLine("");
            }

            if (TransactionList.Count > 0)
            {
                Console.WriteLine("Recent transactions:");
                foreach (var item in TransactionList)
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Press ENTER to go back");
            Console.WriteLine("<--");
            Console.ReadKey();
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"The username; {username}");
            Console.WriteLine("does not exits as a registered user.");
            Console.WriteLine("Check your spelling");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" Press ENTER to go back");
            Console.WriteLine("<--");
            Console.ReadKey();
        }

        private void Banner()
        {
            Console.WriteLine(" ____  _                                _                      _   ");
            Console.WriteLine("/ ___|| |_ _ __ ___  __ _ ___ _   _ ___| |_ ___ _ __ ___   ___| |_ ");
            Console.WriteLine("\\___ \\| __| '__/ _ \\/ _` / __| | | / __| __/ _ \\ '_ ` _ \\ / _ \\ __|");
            Console.WriteLine(" ___) | |_| | |  __/ (_| \\__ \\ |_| \\__ \\ ||  __/ | | | | |  __/ |_ ");
            Console.WriteLine("|____/ \\__|_|  \\___|\\__, |___/\\__, |___/\\__\\___|_| |_| |_|\\___|\\__|");
            Console.WriteLine("                    |___/     |___/                       ");
        }

        public void Ccolor()
        {
            //troll
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            Random rnd = new Random();
            int i = rnd.Next(1, 16);
            Console.ForegroundColor = colors[i];
        }
    }
}