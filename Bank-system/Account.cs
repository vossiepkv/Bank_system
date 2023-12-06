public class Account
{
    private decimal _balance;
    private string _name;

    public Account(string name, decimal balance)
    {
        _name = name;
        _balance = balance;
    }

    public bool Deposit(decimal amount)
    {
        if (amount > 0)
        {
            _balance += amount;
            return true;
        }
        return false;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= _balance)
        {
            _balance -= amount;
            return true;
        }
        return false;
    }

    public void Print()
    {
        Console.WriteLine($"Account Holder: {_name}");
        Console.WriteLine($"Account Balance: {_balance:C}");
    }

    public string Name
    {
        get { return _name; }
    }
}
