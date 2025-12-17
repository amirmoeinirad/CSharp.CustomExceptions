namespace CustomExceptions
{
    // Define a custom exception
    public class InsufficientBalanceException : Exception
    {
        public decimal Balance { get; }
        public decimal RequestedAmount { get; }

        public InsufficientBalanceException(decimal balance, decimal requested)
            : base($"Insufficient balance! Available: {balance}, Requested: {requested}.")
        {
            Balance = balance;
            RequestedAmount = requested;
        }
    }


    // ================================================================


    // Use the custom exception
    public class BankAccount
    {
        public decimal Balance { get; set; }

        public void Withdraw(decimal amount)
        {
            Console.WriteLine("Attempting to withdraw the requested amount from your account...");

            // Custom exception is thrown here
            if (amount > Balance)
                throw new InsufficientBalanceException(Balance, amount);

            // ArgumentOutOfRangeException is a built-in exception
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive!");

            Balance -= amount;

            Console.WriteLine("Success.");
            Console.WriteLine($"New balance: {Balance}");
        }
    }


    // ================================================================


    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Custom Exceptions in C#.NET.");
            Console.WriteLine("----------------------------\n");


            var account = new BankAccount { Balance = 100 };
            Console.WriteLine($"Current Balance: {account.Balance}\n");

            Console.Write("Enter the amount to withdraw: ");
            string requestedAmount = Console.ReadLine()!;
            Console.WriteLine();

            // Handle the custom exception
            // We usually use custom exception with try-catch blocks.
            // The try-catch block allows us to handle exceptions gracefully.
            try
            {                
                account.Withdraw(Convert.ToDecimal(requestedAmount));
            }
            catch (InsufficientBalanceException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"You need {ex.RequestedAmount - ex.Balance} more.");
                Console.WriteLine("Withdrawal failed.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Withdrawal failed.");
            }


            Console.WriteLine("\nDone.");
        }
    }
}
