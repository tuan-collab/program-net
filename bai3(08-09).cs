using System;

namespace OrderProcessing
{

    public class DiscountCalculator
    {
        public decimal ApplyDiscount(decimal totalAmount)
        {
            return totalAmount * 0.95m;
        }

        public decimal ApplyDiscount(
            decimal totalAmount,
            double percentage)
        {
            if (percentage < 0 || percentage > 100)
            {
                throw new ArgumentException(
                    "Percentage phai nam trong khoang 0 den 100."
                );
            }

            decimal discount = totalAmount * (decimal)(percentage / 100);

            return totalAmount - discount;
        }

        public decimal ApplyDiscount(
            decimal totalAmount,
            decimal fixedVoucher,
            decimal minimumOrder)
        {
            if (fixedVoucher < 0)
            {
                throw new ArgumentException(
                    "Gia tri voucher khong duoc am."
                );
            }

            if (minimumOrder < 0)
            {
                throw new ArgumentException(
                    "Gia tri don hang toi thieu khong duoc am."
                );
            }

            if (totalAmount >= minimumOrder)
            {
                return totalAmount - fixedVoucher;
            }

            return totalAmount;
        }
    }


    public class DeliveryService
    {
        public string OrderId { get; set; }
        public double DistanceKm { get; set; }

        public DeliveryService(
            string orderId,
            double distanceKm)
        {
            OrderId = orderId;
            DistanceKm = distanceKm;
        }

        public virtual decimal CalculateShippingFee()
        {
            return (decimal)DistanceKm * 5000m;
        }
    }


    public class ExpressDelivery : DeliveryService
    {
        public ExpressDelivery(
            string orderId,
            double distanceKm)
            : base(orderId, distanceKm)
        {
        }

        public override decimal CalculateShippingFee()
        {
            decimal basicFee =
                (decimal)DistanceKm * 5000m;

            return basicFee * 1.5m + 20_000m;
        }
    }

    public class EcoDelivery : DeliveryService
    {
        public EcoDelivery(
            string orderId,
            double distanceKm)
            : base(orderId, distanceKm)
        {
        }

        public override decimal CalculateShippingFee()
        {
            decimal basicFee =
                (decimal)DistanceKm * 5000m;

            if (DistanceKm > 10)
            {
                return basicFee * 0.9m;
            }

            return basicFee;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("=================================");
                Console.WriteLine("     METHOD OVERLOADING");
                Console.WriteLine("=================================");

                DiscountCalculator calculator =
                    new DiscountCalculator();

                Console.Write("Nhap tong gia tri don hang: ");
                decimal totalAmount =
                    decimal.Parse(Console.ReadLine());

                decimal result1 =
                    calculator.ApplyDiscount(totalAmount);

                Console.WriteLine();
                Console.WriteLine("Giam mac dinh 5%:");
                Console.WriteLine(
                    $"So tien sau giam: {result1:N0} VND"
                );

                Console.WriteLine();
                Console.Write("Nhap phan tram muon giam: ");
                double percentage =
                    double.Parse(Console.ReadLine());

                decimal result2 =
                    calculator.ApplyDiscount(
                        totalAmount,
                        percentage
                    );

                Console.WriteLine(
                    $"So tien sau giam {percentage}%: {result2:N0} VND"
                );

                Console.WriteLine();
                Console.Write("Nhap gia tri voucher: ");
                decimal voucher =
                    decimal.Parse(Console.ReadLine());

                Console.Write("Nhap gia tri don hang toi thieu: ");
                decimal minimumOrder =
                    decimal.Parse(Console.ReadLine());

                decimal result3 =
                    calculator.ApplyDiscount(
                        totalAmount,
                        voucher,
                        minimumOrder
                    );

                Console.WriteLine(
                    $"So tien sau khi ap dung voucher: {result3:N0} VND"
                );


                Console.WriteLine();
                Console.WriteLine("=================================");
                Console.WriteLine("     METHOD OVERRIDING");
                Console.WriteLine("=================================");

                Console.Write("Nhap Order ID: ");
                string orderId =
                    Console.ReadLine();

                Console.Write("Nhap quang duong (km): ");
                double distance =
                    double.Parse(Console.ReadLine());


                DeliveryService normalDelivery =
                    new DeliveryService(
                        orderId,
                        distance
                    );

                ExpressDelivery expressDelivery =
                    new ExpressDelivery(
                        orderId,
                        distance
                    );

                EcoDelivery ecoDelivery =
                    new EcoDelivery(
                        orderId,
                        distance
                    );


                Console.WriteLine();
                Console.WriteLine("=== PHI VAN CHUYEN ===");

                Console.WriteLine(
                    $"Normal Delivery: " +
                    $"{normalDelivery.CalculateShippingFee():N0} VND"
                );

                Console.WriteLine(
                    $"Express Delivery: " +
                    $"{expressDelivery.CalculateShippingFee():N0} VND"
                );

                Console.WriteLine(
                    $"Eco Delivery: " +
                    $"{ecoDelivery.CalculateShippingFee():N0} VND"
                );
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Loi: {ex.Message}");
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Loi: Du lieu nhap vao khong dung dinh dang."
                );
            }
        }
    }
}
