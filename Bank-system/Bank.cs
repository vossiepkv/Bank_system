using TestAccount2;

public class Bank
{
    private List<Account> accounts;
    private List<Transaction> _transactions;

    public Bank()
    {
        accounts = new List<Account>();
        _transactions = new List<Transaction>();
    }

    public void AddAccount(Account account)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account), "Account cannot be null.");
        }

        if (accounts.Any(existingAccount => existingAccount.Name == account.Name))
        {
            throw new InvalidOperationException("An account with the same name already exists.");
        }

        accounts.Add(account);
    }


    public Account GetAccount(string name)
    {
        return accounts.Find(account => account.Name == name);
    }

    public void ExecuteTransaction(DepositTransaction transaction)
    {
        transaction.Execute();
        _transactions.Add(transaction);
    }

    public void ExecuteTransaction(WithdrawTransaction transaction)
    {
        transaction.Execute();
        _transactions.Add(transaction);
    }

    public void ExecuteTransaction(TransferTransaction transaction)
    {
        transaction.Execute();
        _transactions.Add(transaction);
    }

    public void RollbackTransaction(Transaction transaction)
    {
        transaction.Rollback();
        _transactions.Add(transaction);
    }

    public void PrintTransactionHistory()
    {
        Console.WriteLine("Transaction History:");
        for (int i = 0; i < _transactions.Count; i++)
        {
            Console.WriteLine($"{i + 1}. ");
            _transactions[i].Print();
        }
    }


    public bool RollbackTransaction(int index)
    {
        if (index < 0 || index >= _transactions.Count)
            return false;

        Transaction transaction = _transactions[index];
        try
        {
            transaction.Rollback();
            Console.WriteLine("Transaction successfully rolled back.");
            return true;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return false;
        }
    }
}
