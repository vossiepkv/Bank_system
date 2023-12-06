using TestAccount2;

public class TransferTransaction : Transaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private decimal _amount;

    private WithdrawTransaction _withdraw;
    private DepositTransaction _deposit;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _amount = amount;

        _withdraw = new WithdrawTransaction(fromAccount, amount);
        _deposit = new DepositTransaction(toAccount, amount);
    }

    public override void Execute()
    {
        base.Execute();

        if (_withdraw.Executed || _deposit.Executed)
        {
            throw new InvalidOperationException("Transaction has already been executed.");
        }

        _withdraw.Execute();

        if (!_withdraw.Success) // If withdraw failed
        {
            _withdraw.Rollback(); // Rollback the withdrawal
            throw new InvalidOperationException("Withdrawal failed. Transfer not executed.");
        }

        _deposit.Execute(); // Execute the deposit
    }



    public override void Rollback()
    {
        base.Rollback(); // Call base method to update _reversed and _dateStamp

        if (!_withdraw.Executed || !_deposit.Executed)
        {
            throw new InvalidOperationException("Transaction has not been executed.");
        }

        if (_withdraw.Reversed || _deposit.Reversed)
        {
            throw new InvalidOperationException("Transaction has already been reversed.");
        }

    }
}

