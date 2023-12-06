public class WithdrawTransaction : Transaction
{
    private Account _account;
    private decimal _amount;
    private bool _executed;
    private bool _reversed;
    private bool _success; // Add this line

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
        _amount = amount;
    }

    public override void Execute()
    {
        base.Execute(); // Call base method to update _executed and _dateStamp

        if (!_account.Withdraw(_amount))
        {
            _success = false;
            throw new InvalidOperationException("Insufficient funds or invalid withdrawal amount.");
        }
    }

    public override void Rollback()
    {
        base.Rollback(); // Call base method to update _reversed and _dateStamp

        if (_success)
        {
            if (!_account.Deposit(_amount))
            {
                throw new InvalidOperationException("Failed to reverse transaction.");
            }
        }
    }

    public void Print()
    {
        Console.WriteLine($"Withdraw Transaction:");
        Console.WriteLine($"Account: {_account.Name}");
        Console.WriteLine($"Amount: {_amount:C}");
        Console.WriteLine($"Executed: {_executed}");
        Console.WriteLine($"Success: {_success}");
        Console.WriteLine($"Reversed: {_reversed}");
    }
}
