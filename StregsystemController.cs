using Stregsystem.Exceptions;
using Stregsystem.Models;
using System;

namespace Stregsystem
{
    public class StregsystemController
    {
        private IStregsystem Sts;
        private IStregsystemUI Ui;

        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            Ui = ui;
            Sts = stregsystem;
            Ui.CommandEntered += ParseCommand;
        }

        private void ParseCommand(object sender, string command)
        {
            
            string[] CommandArray = command.Split(" ");

            if (command == string.Empty)
            {
                Ui.Ccolor();
                return;
            }
            else if (CommandArray[0] == string.Empty)
            {
                Ui.DisplayGeneralError("Command can't start with a space.");
            }
            else if (CommandArray[0][0] == ':')
            {
                AdminCommands(CommandArray);
            }
            else
            {
                switch (CommandArray.Length)
                {
                    case 1:
                        UserInformationCommand(CommandArray[0]);
                        break;

                    case 2:
                        TransactionCommand(CommandArray);
                        break;

                    case 3:
                        MultiTransactionCommand(CommandArray);
                        break;

                    default:
                        Ui.DisplayTooManyArgumentsError(command);
                        break;
                }
            }
        }

        private void UserInformationCommand(string username)
        {
            try
            {
                User user = Sts.GetUserByUsername(username);
                Ui.DisplayUserInfo(user);
            }
            catch (UsernameNotFoundException)
            {
                Ui.DisplayUserNotFound(username);
            }
            catch (Exception)
            {
                Ui.DisplayGeneralError("");
            }
        }

        private void TransactionCommand(string[] BuyArgs)
        {
            try
            {
                User user = Sts.GetUserByUsername(BuyArgs[0]);
                Product product = Sts.GetActiveProductByID(Convert.ToInt32(BuyArgs[1]));
                BuyTransaction trans = Sts.BuyProduct(1, user, product);
                Ui.DisplayUserBuysProduct(trans);
            }
            catch (UsernameNotFoundException)
            {
                Ui.DisplayUserNotFound(BuyArgs[0]);
            }
            catch (ProductNotFoundException)
            {
                Ui.DisplayProductNotFound(BuyArgs[1]);
            }
            catch (InsufficientCreditsException e)
            {
                Ui.DisplayInsufficientCash(e.eUser, e.eProduct);
            }
            catch (Exception)
            {
                Ui.DisplayGeneralError("");
            }
        }

        private void MultiTransactionCommand(string[] MultiBuyArgs)
        {
            try
            {
                int count = Convert.ToInt32(MultiBuyArgs[1]);
                User user = Sts.GetUserByUsername(MultiBuyArgs[0]);
                Product product = Sts.GetActiveProductByID(Convert.ToInt32(MultiBuyArgs[2]));
                BuyTransaction trans = Sts.BuyProduct(count, user, product);
                Ui.DisplayUserBuysProduct(count, trans);
            }
            catch (UsernameNotFoundException)
            {
                Ui.DisplayUserNotFound(MultiBuyArgs[0]);
            }
            catch (ProductNotFoundException)
            {
                Ui.DisplayProductNotFound(MultiBuyArgs[2]);
            }
            catch (InsufficientCreditsException e)
            {
                int Count = Convert.ToInt32(MultiBuyArgs[1]);
                Ui.DisplayInsufficientCash(Count, e.eUser, e.eProduct);
            }
            catch (Exception)
            {
                Ui.DisplayGeneralError("");
            }
        }
     
        private void AdminCommands(string[] commands)
        {
            Product prod;
            try
            {
                switch (commands[0])
                {
                    case ":q":
                    case ":quit":
                        Ui.Close();
                        break;

                    case ":activate":
                        prod = Sts.GetProductByID(Convert.ToInt32(commands[1]));
                        prod.Active = true;
                        break;

                    case ":deactivate":
                        prod = Sts.GetActiveProductByID(Convert.ToInt32(commands[1]));
                        prod.Active = false;
                        break;

                    case ":crediton":
                        prod = Sts.GetActiveProductByID(Convert.ToInt32(commands[1]));
                        prod.CanBeBoughtOnCredit = true;
                        break;

                    case ":creditoff":
                        prod = Sts.GetActiveProductByID(Convert.ToInt32(commands[1]));
                        prod.CanBeBoughtOnCredit = false;
                        break;

                    case ":addcredits":
                        User user = Sts.GetUserByUsername(commands[1]);
                        decimal amount = Convert.ToDecimal(commands[2]) * 100;
                        Sts.AddCreditsToAccount(user, amount);
                        break;

                    default:
                        Ui.DisplayAdminCommandNotFoundMessage(commands[0]);
                        break;
                }
            }
            catch (UsernameNotFoundException)
            {
                Ui.DisplayUserNotFound(commands[1]);
            }
            catch (ProductNotFoundException)
            {
                Ui.DisplayProductNotFound(commands[1]);
            }
            catch (Exception)
            {
                Ui.DisplayGeneralError("\n" +
                    "OBS\n" +
                    "Admincommands need to be in the right format.\n" +
                    ":[Command] [Id]\n" +
                    "or\n" +
                    ":[Command] [Username] [Amount]\n" +
                    "\n\n" +
                    "Normal usage:");
            }
        }
        
        
    }
}