using System;
using TestAccount2;

namespace BankSystem;


public enum MenuOption
{
    Withdraw = 1,
    Deposit = 2,
    Print = 3,
    Transfer = 4,
    AddAccount = 5,
    PrintHistory = 6, 
    Rollback = 7, 
    Exit = 8 
}




public static class BankSystem
{

    public static Account FindAccount(Bank bank)
    {
        Console.WriteLine("Enter the account name:");
        string accountName = Console.ReadLine();

        Account account = bank.GetAccount(accountName);

        if (account == null)
        {
            Console.WriteLine($"Account '{accountName}' not found.");
        }

        return account;
    }



    public static MenuOption ReadUserOption()
    {
        int choice;
        do
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Print");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Add Account");
            Console.WriteLine("6. Print History");
            Console.WriteLine("7. Roll Back");
            Console.WriteLine("8. Exit");

            if (int.TryParse(Console.ReadLine(), out choice) && Enum.IsDefined(typeof(MenuOption), choice))
            {
                return (MenuOption)choice;
            }
            Console.WriteLine("Invalid choice. Please select a valid option.");
        } while (true);
    }

    public static void DoDeposit(Bank bank)
    {
        Console.WriteLine("Enter the account name for deposit:");
        Account account = FindAccount(bank);

        if (account != null)
        {
            Console.WriteLine("Enter the amount to deposit:");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                // Create a deposit transaction and execute it
                DepositTransaction depositTransaction = new DepositTransaction(account, amount);
                bank.ExecuteTransaction(depositTransaction);

                Console.WriteLine("Deposit successful.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal value.");
            }
        }
    }



    public static void DoWithdraw(Bank bank)
    {
        Console.WriteLine("Enter the account name for withdrawal:");
        Account account = FindAccount(bank);

        if (account != null)
        {
            Console.WriteLine("Enter the amount to withdraw:");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                // Create a withdraw transaction and execute it
                WithdrawTransaction withdrawTransaction = new WithdrawTransaction(account, amount);
                bank.ExecuteTransaction(withdrawTransaction);

                Console.WriteLine("Withdrawal successful.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal value.");
            }
        }
    }



    public static void DoTransfer(Bank bank)
    {
        Console.WriteLine("Enter the source account name for transfer:");
        Account fromAccount = FindAccount(bank);

        if (fromAccount != null)
        {
            Console.WriteLine("Enter the destination account name for transfer:");
            Account toAccount = FindAccount(bank);

            if (toAccount != null)
            {
                Console.WriteLine("Enter the amount to transfer:");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    // Create a transfer transaction and execute it
                    TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, amount);
                    bank.ExecuteTransaction(transferTransaction);

                    Console.WriteLine("Transfer successful.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal value.");
                }
            }
            else
            {
                Console.WriteLine("Destination account not found.");
            }
        }
    }


    public static void AddNewAccount(Bank bank)
    {
        Console.WriteLine("Enter the account name:");
        string accountName = Console.ReadLine();

        Console.WriteLine("Enter the initial balance:");
        if (decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
        {
            Account newAccount = new Account(accountName, initialBalance);
            bank.AddAccount(newAccount);
            Console.WriteLine($"Account '{accountName}' with an initial balance of {initialBalance:C} added.");
        }
        else
        {
            Console.WriteLine("Invalid initial balance. Please enter a valid decimal value.");
        }
    }




    public static void DoPrint(Bank bank)
    {
        Console.WriteLine("Enter the account name to print:");
        Account account = FindAccount(bank);

        if (account != null)
        {
            // Call the Print method of the account
            account.Print();
        }
    }


    public static void PrintTransactionHistory(Bank bank)
    {
        bank.PrintTransactionHistory();
    }

    public static void DoRollback(Bank bank)
    {
        Console.WriteLine("Enter the index of the transaction to rollback:");
        if (int.TryParse(Console.ReadLine(), out int index))
        {
            bool success = bank.RollbackTransaction(index - 1);
            if (success)
            {
                Console.WriteLine("Transaction successfully rolled back.");
            }
            else
            {
                Console.WriteLine("Error rolling back transaction. Please check the index and try again.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }
    }


}
