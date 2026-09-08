using System;

namespace PaymentGatewayDemo
{
    // Interface: kha nang thanh toan
    public interface IPayable
    {
        bool ProcessPayment(decimal amount);
    }

    // Interface: kha nang hoan tien
    public interface IRefundable
    {
        bool ProcessRefund(decimal amount, string reason);
    }

    // Abstract Class
    public abstract class PaymentGateway
    {
        public string TransactionId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Status { get; protected set; }

        protected PaymentGateway(string transactionId)
        {
            TransactionId = transactionId;
            CreationDate = DateTime.Now;
            Status = "Pending";
        }

        public abstract void ValidateConnection();

        public virtual void LogTransaction(string message)
        {
            Console.WriteLine(
                $"[Transaction: {TransactionId}] {message}"
            );
        }
    }

    // MomoPayment ke thua PaymentGateway
    // va trien khai 2 interface
    public class MomoPayment : PaymentGateway, IPayable, IRefundable
    {
        public string PhoneNumber { get; set; }

        public MomoPayment(string transactionId, string phoneNumber)
            : base(transactionId)
        {
            PhoneNumber = phoneNumber;
        }

        public override void ValidateConnection()
        {
            Console.WriteLine("Dang kiem tra ket noi API MoMo...");
            Console.WriteLine("Ket noi API MoMo thanh cong.");
        }

        public bool ProcessPayment(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("So tien thanh toan phai lon hon 0.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                Console.WriteLine("So dien thoai khong hop le.");
                return false;
            }

            Status = "Success";

            LogTransaction(
                $"Thanh toan thanh cong {amount:N0} VND"
            );

            return true;
        }

        public bool ProcessRefund(decimal amount, string reason)
        {
            if (amount <= 0)
            {
                Console.WriteLine("So tien hoan phai lon hon 0.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(reason))
            {
                Console.WriteLine("Ly do hoan tien khong duoc de trong.");
                return false;
            }

            Status = "Refunded";

            LogTransaction(
                $"Hoan tien {amount:N0} VND. Ly do: {reason}"
            );

            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Nhap ma giao dich: ");
                string transactionId = Console.ReadLine();

                Console.Write("Nhap so dien thoai MoMo: ");
                string phoneNumber = Console.ReadLine();

                MomoPayment payment =
                    new MomoPayment(transactionId, phoneNumber);

                Console.WriteLine("\n--- THONG TIN GIAO DICH ---");
                Console.WriteLine($"Transaction ID: {payment.TransactionId}");
                Console.WriteLine($"Creation Date: {payment.CreationDate}");
                Console.WriteLine($"Status: {payment.Status}");

                Console.WriteLine("\n--- KIEM TRA KET NOI ---");
                payment.ValidateConnection();

                Console.Write("\nNhap so tien thanh toan: ");
                decimal paymentAmount = decimal.Parse(Console.ReadLine());

                Console.WriteLine("\n--- THANH TOAN ---");
                bool paymentResult =
                    payment.ProcessPayment(paymentAmount);

                Console.WriteLine(
                    $"Ket qua: {(paymentResult ? "Thanh cong" : "That bai")}"
                );

                Console.WriteLine($"Status: {payment.Status}");

                Console.Write("\nBan co muon hoan tien? (y/n): ");
                string answer = Console.ReadLine();

                if (answer.ToLower() == "y")
                {
                    Console.Write("Nhap so tien hoan: ");
                    decimal refundAmount =
                        decimal.Parse(Console.ReadLine());

                    Console.Write("Nhap ly do hoan tien: ");
                    string reason = Console.ReadLine();

                    Console.WriteLine("\n--- HOAN TIEN ---");

                    bool refundResult =
                        payment.ProcessRefund(refundAmount, reason);

                    Console.WriteLine(
                        $"Ket qua: {(refundResult ? "Thanh cong" : "That bai")}"
                    );

                    Console.WriteLine($"Status: {payment.Status}");
                }

                Console.WriteLine("\n--- KET THUC ---");
                payment.LogTransaction("Ket thuc giao dich.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Du lieu nhap vao khong dung dinh dang.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }
        }
    }
}