using System;
using System.Collections.Generic;

namespace ExamSET01
{
    // =======================
    // QUESTION 1: BANK SYSTEM
    // =======================

    public interface IAccount
    {
        void CheckBalance();
        void Transfer(decimal amount);
    }

    public abstract class Account : IAccount
    {
        protected decimal balance;

        public Account(decimal initialBalance)
        {
            balance = initialBalance;
        }

        public abstract void CheckBalance();

        public virtual void Transfer(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid transfer amount!");
                return;
            }
            if (balance >= amount)
            {
                balance -= amount;
                Console.WriteLine($"Your transferred {amount:N0} đ, Your balancer: {balance:N0} đ");
            }
            else
            {
                Console.WriteLine("Not enough balance!");
            }
        }
    }

    public class NormalAccount : Account
    {
        public NormalAccount(decimal initialBalance) : base(initialBalance) { }

        public override void CheckBalance()
        {
            Console.WriteLine($"Your balancer: {balance:N0} đ");
        }
    }

    public class ExchangeAccount : Account
    {
        private decimal exchangeRate;
        private decimal amount; // số USD

        public ExchangeAccount(decimal amount, decimal exchangeRate)
            : base(amount * exchangeRate)
        {
            this.amount = amount;
            this.exchangeRate = exchangeRate;
        }

        public override void CheckBalance()
        {
            Console.WriteLine($"Your balancer: {balance:N0} đ (ExchangeRate: {exchangeRate:N0}, Amount: {amount})");
        }
    }

    // =======================
    // QUESTION 2: MUSIC STORE
    // =======================

    public interface IInstrument
    {
        void Play();
    }

    public abstract class Instrument : IInstrument
    {
        public string Name { get; set; }
        public int Year { get; set; }

        public Instrument(string name, int year)
        {
            Name = name;
            Year = year;
        }

        public abstract void Play();
        public abstract void ShowInfo();
    }

    public class Guitar : Instrument
    {
        public int Strings { get; set; }

        public Guitar(string name, int year, int strings) : base(name, year)
        {
            Strings = strings;
        }

        public override void Play()
        {
            Console.WriteLine($"{Name} is playing with {Strings} strings...");
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Instrument: {Name}, Year: {Year}, Strings: {Strings}");
        }
    }

    public class Piano : Instrument
    {
        public int Keys { get; set; }

        public Piano(string name, int year, int keys) : base(name, year)
        {
            Keys = keys;
        }

        public override void Play()
        {
            Console.WriteLine($"{Name} is playing with {Keys} keys...");
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Instrument: {Name}, Year: {Year}, Keys: {Keys}");
        }
    }

    // =======================
    // MAIN PROGRAM
    // =======================
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== MAIN MENU ===");
                Console.WriteLine("1. Bank Account System");
                Console.WriteLine("2. Music Store");
                Console.WriteLine("0. Exit");
                Console.Write("Choose option: ");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 0) break;

                switch (choice)
                {
                    case 1:
                        RunBankSystem();
                        break;
                    case 2:
                        RunMusicStore();
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        // Run Bank Account
        static void RunBankSystem()
        {
            Console.WriteLine("\n=== BANK ACCOUNT SYSTEM ===");
            Console.WriteLine("1. Normal Account");
            Console.WriteLine("2. Exchange Account (USD -> VND)");
            Console.Write("Choose account type: ");
            int choice = int.Parse(Console.ReadLine());

            Account account;
            if (choice == 1)
            {
                Console.Write("Enter initial balance (VND): ");
                decimal init = decimal.Parse(Console.ReadLine());
                account = new NormalAccount(init);
            }
            else
            {
                Console.Write("Enter amount in USD: ");
                decimal usd = decimal.Parse(Console.ReadLine());
                Console.Write("Enter exchange rate (USD->VND): ");
                decimal rate = decimal.Parse(Console.ReadLine());
                account = new ExchangeAccount(usd, rate);
            }

            account.CheckBalance();

            Console.Write("Enter amount to transfer (VND): ");
            decimal transfer = decimal.Parse(Console.ReadLine());
            account.Transfer(transfer);

            account.CheckBalance();
        }

        // Run Music Store
        static void RunMusicStore()
        {
            Console.WriteLine("\n=== MUSIC STORE ===");

            List<Instrument> instruments = new List<Instrument>()
            {
                new Guitar("Yamaha Guitar", 2020, 6),
                new Guitar("Fender Guitar", 2019, 5),
                new Piano("Casio Piano", 2021, 88),
                new Piano("Yamaha Piano", 2022, 76),
                new Guitar("Taylor Guitar", 2023, 6)
            };

            foreach (var ins in instruments)
            {
                ins.ShowInfo();
                ins.Play();
                Console.WriteLine("----------------------");
            }
        }
    }
}
