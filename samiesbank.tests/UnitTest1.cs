using samiesbank.Models;
using System;
using System.Collections.Generic;
using Xunit;


namespace samiesbank.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void WithdrawalOverDraft()
        {
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Name = "Samie",
                    CustomerId = 1,
                    Accounts = new List<Account>()
                    {
                        new Account()
                        {
                            AccountID = 1,
                            Balance = 700M
                        }
                    }
                }
            };

            BankRepository.AddCustomers(customers);
            decimal amount = 900M;
            decimal expected = 700M;

            var actualAccount = BankRepository.Withdrawal(1, amount);

            Assert.Equal(expected, actualAccount.Balance, 2);
        }



        [Fact]
        public void Withdrawal_CantWithdrawFromNonExistingAccount()
        {
            // Arrange
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Name = "Samie",
                    CustomerId = 1,
                    Accounts = new List<Account>()
                    {
                        new Account()
                        {
                            AccountID = 3,
                            Balance = 500M
                        }
                    }
                }
            };
            BankRepository.AddCustomers(customers);
            decimal amount = 100M;
            string expected = BankRepository.AccountDoesNotExist;

            // Act
            BankRepository.Withdrawal(1, amount);

            // Assert
            Assert.Equal(expected, BankRepository.Errormessage);
        }


        [Fact]
        public void Deposit_CanMakeBasicDeposit()
        {
            // Arrange
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Name = "Samie",
                   CustomerId = 1,
                    Accounts = new List<Account>()
                    {
                        new Account()
                        {
                            AccountID = 4,
                            Balance = 600M
                        }
                    }
                }
            };
            BankRepository.AddCustomers(customers);
            decimal amount = 200M;
            decimal expected = 800M;

            // Act
            var account = BankRepository.Deposit(4, amount);

            // Assert
            Assert.Equal(expected, account.Balance, 2);
        }

        [Fact]
        public void Deposit_CantDepositNegativeAmount()
        {
            // Arrange
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Name = "Robert",
                    CustomerId = 2,
                    Accounts = new List<Account>()
                    {
                        new Account()
                        {
                            AccountID = 5,
                            Balance = 600M
                        }
                    }
                }
            };
            BankRepository.AddCustomers(customers);
            decimal amount = -200M;
            decimal expected = 600M;

            // Act
            var account = BankRepository.Deposit(5, amount);

            // Assert
            Assert.Equal(expected, account.Balance, 2);
        }

        [Fact]
        public void Deposit_CantDepositToNonExistingAccount()
        {
            // Arrange
            var customers = new List<Customer>()
            {
                new Customer()
                {
                    Name = "Michael",
                    CustomerId = 3,
                    Accounts = new List<Account>()
                    {
                        new Account()
                        {
                            AccountID = 3,
                            Balance = 600M
                        }
                    }
                }
            };
            BankRepository.AddCustomers(customers);
            decimal amount = 200M;
            string expected = BankRepository.AccountDoesNotExist;

            // Act
            BankRepository.Deposit(1, amount);

            // Assert
            Assert.Equal(expected, BankRepository.Errormessage);
        }

        [Fact]
        public void Transfer()
        {
            // Arrange
            var fromAccount = new Account() { AccountID = 5, Balance = 600M };
            var toAccount = new Account() { AccountID = 6, Balance = 700M }; 
            decimal amount = 200M;
            decimal expectedFromAccountBalance = 400M;
            decimal expectedToAccountBalance = 900M;

            // Act
            var account = fromAccount.Transfer(amount, fromAccount, toAccount);

            // Assert
            Assert.Equal(expectedFromAccountBalance, fromAccount.Balance);
            Assert.Equal(expectedToAccountBalance, toAccount.Balance);
        }

        [Fact]
        public void TransferMoreThanAvalible()
        {
            // Arrange
            var fromAccount = new Account() { AccountID = 5, Balance = 600M };
            var toAccount = new Account() { AccountID = 6, Balance = 700M };
            decimal amount = 650M;
            decimal expected = 600M;

            fromAccount.Transfer(amount, fromAccount, toAccount);
            Assert.Equal(expected, fromAccount.Balance);
        }

        [Fact]
        public void Transfer_AtLeastOneOfTheAccountsDontExist()
        {
            // Arrange
            Account fromAccount = null;
            var toAccount = new Account() { AccountID = 55, Balance = 400M };
            string expected = Account.AtLeastOneOfTheAccountsDoesntExist;
            decimal amount = 500M;

            // Act
            var account = new Account();
            account.Transfer(amount, fromAccount, toAccount);

            // Assert
            Assert.Equal(expected, account.ErrorMessage);
        }

        [Fact]
        public void Transfer_CantTransferNegativeAmounts()
        {
            // Arrange
            var fromAccount = new Account() { AccountID = 99, Balance = 500M };
            var toAccount = new Account() { AccountID = 7777, Balance = 400M };
            var amount = -500M;
            var expectedError = Account.CantTransferNegativeAmounts;
            var expectedBalanceInFromAccount = 500M;
            var expectedBalanceInInAccount = 400M;


            // Act
            var account = fromAccount.Transfer(amount, fromAccount, toAccount);

            // Assert
            Assert.Equal(expectedBalanceInFromAccount, fromAccount.Balance, 2);
            Assert.Equal(expectedBalanceInInAccount, toAccount.Balance, 2);
            Assert.Equal(expectedError, account.ErrorMessage);
        }

        [Fact]
        public void Transfer_CantTransferToSameAccount()
        {
            // Arrange
            var account = new Account() { AccountID = 888, Balance = 500M };
            var amount = 300M;
            var expected = Account.CantTransferBetweenSameAccounts;

            // Act
            account.Transfer(amount, account, account);

            // Assert
            Assert.Equal(expected, account.ErrorMessage);
        }
    }
}
