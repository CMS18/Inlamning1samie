using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samiesbank.Models
{

    public class Account
    {
        public string ErrorMessage = "";
        public string SuccessMessage = "";

        public const string AmountLargerThanBalanceInFromAccount = "You can't transfer more than the balance of the Account.";
        public const string CantTransferBetweenSameAccounts = "You can't transfer between the same account.";
        public const string AtLeastOneOfTheAccountsDoesntExist = "At least one of the accounts don't exist.";
        public const string CantTransferNegativeAmounts = "You can't transfer negative amounts.";

        public int AccountID { get; set; }

        public decimal Balance { get; set; }

        public Account Transfer(decimal amount, Account fromAccount, Account toAccount)
        {
            if (amount >= 0)
            {
                if (fromAccount == null || toAccount == null)
                {
                    ErrorMessage = AtLeastOneOfTheAccountsDoesntExist;
                    SuccessMessage = "";
                }
                else
                {
                    if (fromAccount != toAccount)
                    {
                        if (amount < fromAccount.Balance)
                        {
                            fromAccount.Balance -= amount;
                            toAccount.Balance += amount;
                            SuccessMessage = $"You successfully transfered {amount} SEK from account {fromAccount.AccountID} to account {toAccount.AccountID}.";
                            ErrorMessage = "";
                        }
                        else
                        {
                            ErrorMessage = AmountLargerThanBalanceInFromAccount;
                            SuccessMessage = "";
                        }
                    }
                    else
                    {
                        ErrorMessage = CantTransferBetweenSameAccounts;
                        SuccessMessage = "";
                    }
                }
            }
            else
            {
                ErrorMessage = CantTransferNegativeAmounts;
                SuccessMessage = "";
            }
            return fromAccount;
        }
    }
}