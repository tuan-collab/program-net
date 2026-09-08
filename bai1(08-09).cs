using System;

namespace BankAccountDemo
{
    public class BankAccount
    {
        private static long _nextAccountNumber = 1000000001;

        private const decimal MinimumBalance = 50_000m;

        private decimal _balance;
        private string _accountHolder;

        public long AccountNumber { get; private set; }

        public string AccountHolder
        {
            get
            {
                return _accountHolder;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(
                        "Ten chu tai khoan khong duoc de trong."
                    );
                }

                _accountHolder = value;
            }
        }

        // So du: chi cho phep doc
        public decimal Balance
        {
            get
            {
                return _balance;
            }
        }

        // Constructor
        public BankAccount(string accountHolder, decimal initialBalance)
        {
            // Kiem tra ten chu tai khoan
            if (string.IsNullOrWhiteSpace(accountHolder))
            {
                throw new ArgumentException(
                    "Ten chu tai khoan khong duoc de trong."
                );
            }

            // Kiem tra so du ban dau
            if (initialBalance < MinimumBalance)
            {
                throw new ArgumentException(
                    $"So du ban dau phai >= {MinimumBalance:N0} VND."
                );
            }

            // Tu dong cap so tai khoan
            AccountNumber = _nextAccountNumber++;

            // Gan thong tin
            AccountHolder = accountHolder;
            _balance = initialBalance;
        }

        // Nap tien
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(
                    "So tien nap phai lon hon 0."
                );
            }

            _balance += amount;
        }

        // Rut tien
        public bool Withdraw(decimal amount)
        {
            // So tien rut phai > 0
            if (amount <= 0)
            {
                throw new ArgumentException(
                    "So tien rut phai lon hon 0."
                );
            }

            // So du sau khi rut khong duoc nho hon 50,000
            if (_balance - amount < MinimumBalance)
            {
                return false;
            }

            _balance -= amount;
            return true;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("----- THONG TIN TAI KHOAN -----");
            Console.WriteLine($"So tai khoan : {AccountNumber}");
            Console.WriteLine($"Chu tai khoan: {AccountHolder}");
            Console.WriteLine($"So du        : {Balance:N0} VND");
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Nhap thong tin tai khoan 1
                Console.Write("Nhap ten chu tai khoan 1: ");
                string name1 = Console.ReadLine();

                Console.Write("Nhap so du ban dau: ");
                decimal balance1 = decimal.Parse(Console.ReadLine());

                BankAccount account1 = new BankAccount(name1, balance1);

                // Nhap thong tin tai khoan 2
                Console.Write("Nhap ten chu tai khoan 2: ");
                string name2 = Console.ReadLine();

                Console.Write("Nhap so du ban dau: ");
                decimal balance2 = decimal.Parse(Console.ReadLine());

                BankAccount account2 = new BankAccount(name2, balance2);

                // Hien thi thong tin
                Console.WriteLine("\n=== THONG TIN TAI KHOAN ===");
                account1.DisplayInfo();
                account2.DisplayInfo();

                // ==========================
                // DEPOSIT
                // ==========================

                Console.Write("Nhap so tien muon nap vao tai khoan 1: ");
                decimal depositAmount = decimal.Parse(Console.ReadLine());

                account1.Deposit(depositAmount);

                Console.WriteLine("Nap tien thanh cong!");
                account1.DisplayInfo();

                // WITHDRAW

                Console.Write("Nhap so tien muon rut: ");
                decimal withdrawAmount = decimal.Parse(Console.ReadLine());

                bool result = account1.Withdraw(withdrawAmount);

                if (result)
                {
                    Console.WriteLine("Rut tien thanh cong!");
                }
                else
                {
                    Console.WriteLine(
                        "Rut tien that bai! So du phai >= 50,000 VND."
                    );
                }

                account1.DisplayInfo();
                // TEST EXCEPTION
                Console.WriteLine("=== TEST TAI KHOAN KHONG HOP LE ===");

                Console.Write("Nhap ten tai khoan 3: ");
                string name3 = Console.ReadLine();

                Console.Write("Nhap so du tai khoan 3: ");
                decimal balance3 = decimal.Parse(Console.ReadLine());

                BankAccount account3 = new BankAccount(name3, balance3);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }
        }
    
    }
}