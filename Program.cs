using System;
using System.IO;
using System.Threading;

namespace Stregsystem
{
    public class Program
    {
        static void Main(string[] args)
        {


            IStregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem); 
            StregsystemController sc = new StregsystemController(ui, stregsystem); 

            string datapath = "..\\..\\..\\Data\\";

            stregsystem.LoadProductsList(datapath + "productsReformated.csv");
            stregsystem.LoadUserList(datapath + "users.csv");
            // datapath for LogFile.txt is in "stregsystem"


            //Debugging old school style // ghetto testing
            #region 


            //var iss = stregsystem.GetUserByUsername("lcall");
            //var ist = stregsystem.GetUserByUsername("rking");
            //var prd = stregsystem.GetProductByID(32);

            //Console.WriteLine(iss);
            //Console.WriteLine(ist);

            //for (int i = 0; i < 4; i++)
            //{
            //    stregsystem.AddCreditsToAccount(ist, 2000);
            //    stregsystem.BuyProduct(ist, prd);
            //    Thread.Sleep(1100);
            //}

            //stregsystem.AddCreditsToAccount(ist, 3000);
            //stregsystem.BuyProduct(ist, prd);

            //Console.WriteLine(iss);
            //Console.WriteLine(ist);


            //foreach (var item in stregsystem.GetTransactions(iss, 5))
            //{
            //    Console.WriteLine(item);
            //}


            #endregion

            ui.Start();


            
        }
    }
}
