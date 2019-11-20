using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samiesbank.Models
{
    public class BankRepository
    {
        public static string Errormessage = "";
        public static string SuccessMessage = "";

        public const string AccountDoesNotExist = "No account with such an ID exists.";

        private static List<Customer> Customers { get; set; }
        public static List<Customer> GetCustomers()
        {
            return Customers;
        }

        public static void AddCustomers(List<Customer> customers)
        {
            Customers = customers;
        }

        public static List<Account> GetAccounts()
        {
            List<Account> accounts = new List<Account>();
            for (int i = 0; i < Customers.Count; i++)
            {
                accounts.AddRange(Customers[i].Accounts);
            }
            return accounts;
        }

        public static Account Withdrawal(int accountId, decimal amount)
        {
            var account = GetAccount(accountId);

            if (amount < 0)
            {
                Errormessage = "You can't widthdraw negative amounts";
            }
            else
            {
                if (account != null)
                {
                    if (amount <= account.Balance)
                    {
                        account.Balance -= amount;
                        SuccessMessage = $"You successfully withdrew {amount} SEK from account {accountId}. Current balance: {account.Balance} SEK.";
                        Errormessage = "";
                    }
                    else
                    {
                        Errormessage = "You can't widthdraw more than the balance of the account.";
                        SuccessMessage = "";
                    }
                }
                else
                {
                    Errormessage = AccountDoesNotExist;
                    SuccessMessage = "";
                }
            }

            return account;
        }

        public static Account Deposit(int accountId, decimal amount)
        {
            var account = GetAccount(accountId);

            if (account != null)
            {
                if (amount < 0)
                {
                    Errormessage = "You can't deposit negative amounts.";
                    SuccessMessage = "";
                }
                else
                {
                    account.Balance += amount;
                    SuccessMessage = $"You successfully deposited {amount} SEK to account {accountId}. Current balance: {account.Balance} SEK.";
                    Errormessage = "";
                }
            }
            else
            {
                Errormessage = AccountDoesNotExist;
                SuccessMessage = "";
            }

            return account;
        }

        private static Account GetAccount(int accountId)
        {
            var account = GetAccounts().Find(a => a.AccountID == accountId);
            return account;
        }
        /*
        public static bool Transfer(decimal amount, int fromAccountId, int toAccountId)
        {

            var accountTo = GetAccount(toAccountId);
            var accountFrom = GetAccount(fromAccountId);
            if (accountFrom == null || accountTo == null)
            {
                Errormessage = "The account could not be found. Make sure both account numbers are correct.";
                SuccessMessage = "";
            }
            else
            {
                if (accountFrom.Balance - amount < 0)
                {
                    Errormessage =
                        $"The amount you're transferring can't be higher than your balance. Your current balance is {accountFrom.Balance}";
                    SuccessMessage = "";
                }
                else
                {
                    if (accountFrom.AccountID == accountTo.AccountID)
                    {
                        Errormessage =
                            "The account you're sending money to can't be the same account you're deducting money from.";
                        SuccessMessage = "";
                    }
                    else
                    {
                        if (amount < 0)
                        {
                            Errormessage = $"You can't transfer negative amount of {amount} from {accountFrom.AccountID} to {accountTo.AccountID}";
                            SuccessMessage = "";
                        }
                        else
                        {
                            accountFrom.Balance -= amount;
                            accountTo.Balance += amount;
                            Errormessage = "";
                            SuccessMessage =
                                $"Congratulations you've transferred {amount} to account # {accountTo.AccountID} and the new balance is {accountTo.Balance}";
                        }
                    }
                }
            }

            return true;
        }
        */
    }
}

