using System;
using System.Numerics;
using System.Transactions;

namespace TestAccount2
{
    public class DepositTransaction : Transaction
    {
        private Account _account;
        private bool _success;

        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
        }

        public void Print()
        {
            Console.WriteLine($"Deposit Transaction:");
            Console.WriteLine($"Account: {_account.Name}");
            Console.WriteLine($"Amount: {_amount:C}");
            Console.WriteLine($"Executed: {base.Executed}");
            Console.WriteLine($"Success: {_success}");
            Console.WriteLine($"Reversed: {base.Reversed}");
        }

        public override void Execute()
        {
            base.Execute();

            if (_amount > 0)
            {
                _account.Deposit(_amount);
                _success = true;
            }
            else
            {
                _success = false;
            }
        }

        public override void Rollback()
        {
            base.Rollback(); // Call base method to update _reversed and _dateStamp

            if (_success)
            {
                if (!_account.Withdraw(_amount))
                {
                    throw new InvalidOperationException("Failed to reverse transaction.");
                }
            }
        }

        public bool Success => _success;
    }
}
