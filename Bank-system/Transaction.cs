public abstract class Transaction
{
    protected decimal _amount;
    private bool _executed = false;
    private bool _reversed = false;
    protected DateTime _dateStamp;
    protected bool _success;

    public decimal Amount => _amount;
    public bool Success => _executed && !_reversed;
    public bool Executed => _executed;
    public bool Reversed => _reversed;
    public DateTime DateStamp => _dateStamp;

    public Transaction(decimal amount)
    {
        _amount = amount;
        _dateStamp = DateTime.Now;
    }

    public virtual void Print()
    {
        Console.WriteLine($"Transaction: {_amount:C}, Executed: {_executed}, Reversed: {_reversed}, Date: {_dateStamp}");
    }

    public virtual void Execute()
    {
        if (_executed)
        {
            throw new InvalidOperationException("Transaction has already been executed.");
        }

        _executed = true;
        _dateStamp = DateTime.Now;
    }


    public virtual void Rollback()
    {
        _reversed = true;
        _dateStamp = DateTime.Now;
    }
}
