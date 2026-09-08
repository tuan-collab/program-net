using System;

namespace EmployeeHierarchy
{
    public class Person
    {
        public string Id { get; private set; }
        public string FullName { get; set; }
        public int BirthYear { get; set; }

        public Person(string id, string fullName, int birthYear)
        {
            Id = id;
            FullName = fullName;
            BirthYear = birthYear;
        }

        public int GetAge(int currentYear)
        {
            return currentYear - BirthYear;
        }
    }

    public class Employee : Person
    {
        public decimal BaseSalary { get; set; }

        public Employee(
            string id,
            string fullName,
            int birthYear,
            decimal baseSalary)
            : base(id, fullName, birthYear)
        {
            BaseSalary = baseSalary;
        }

        // Tinh thu nhap
        public virtual decimal CalculateIncome()
        {
            return BaseSalary;
        }
    }

    // Manager ke thua Employee
    // sealed: khong cho class khac ke thua Manager
    public sealed class Manager : Employee
    {
        public decimal ResponsibilityAllowance { get; set; }

        // Constructor cua Manager
        public Manager(
            string id,
            string fullName,
            int birthYear,
            decimal baseSalary,
            decimal allowance)
            : base(id, fullName, birthYear, baseSalary)
        {
            ResponsibilityAllowance = allowance;
        }

        // Ghi de CalculateIncome()
        public override decimal CalculateIncome()
        {
            return BaseSalary + ResponsibilityAllowance;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // ==========================
            // NHAP THONG TIN EMPLOYEE
            // ==========================

            Console.WriteLine("=== NHAP THONG TIN EMPLOYEE ===");

            Console.Write("Nhap ID: ");
            string employeeId = Console.ReadLine();

            Console.Write("Nhap ho ten: ");
            string employeeName = Console.ReadLine();

            Console.Write("Nhap nam sinh: ");
            int employeeBirthYear =
                int.Parse(Console.ReadLine());

            Console.Write("Nhap luong co ban: ");
            decimal employeeSalary =
                decimal.Parse(Console.ReadLine());

            Employee employee = new Employee(
                employeeId,
                employeeName,
                employeeBirthYear,
                employeeSalary
            );

            // ==========================
            // NHAP THONG TIN MANAGER
            // ==========================

            Console.WriteLine();
            Console.WriteLine("=== NHAP THONG TIN MANAGER ===");

            Console.Write("Nhap ID: ");
            string managerId = Console.ReadLine();

            Console.Write("Nhap ho ten: ");
            string managerName = Console.ReadLine();

            Console.Write("Nhap nam sinh: ");
            int managerBirthYear =
                int.Parse(Console.ReadLine());

            Console.Write("Nhap luong co ban: ");
            decimal managerSalary =
                decimal.Parse(Console.ReadLine());

            Console.Write("Nhap phu cap trach nhiem: ");
            decimal allowance =
                decimal.Parse(Console.ReadLine());

            Manager manager = new Manager(
                managerId,
                managerName,
                managerBirthYear,
                managerSalary,
                allowance
            );

            // ==========================
            // HIEN THI THONG TIN
            // ==========================

            Console.WriteLine();
            Console.WriteLine("=== THONG TIN EMPLOYEE ===");

            Console.WriteLine($"ID: {employee.Id}");
            Console.WriteLine($"Ho ten: {employee.FullName}");
            Console.WriteLine($"Nam sinh: {employee.BirthYear}");

            Console.Write("Nhap nam hien tai: ");
            int currentYear =
                int.Parse(Console.ReadLine());

            Console.WriteLine(
                $"Tuoi: {employee.GetAge(currentYear)}"
            );

            Console.WriteLine(
                $"Thu nhap: {employee.CalculateIncome():N0} VND"
            );

            // ==========================
            // HIEN THI MANAGER
            // ==========================

            Console.WriteLine();
            Console.WriteLine("=== THONG TIN MANAGER ===");

            Console.WriteLine($"ID: {manager.Id}");
            Console.WriteLine($"Ho ten: {manager.FullName}");
            Console.WriteLine($"Nam sinh: {manager.BirthYear}");
            Console.WriteLine(
                $"Tuoi: {manager.GetAge(currentYear)}"
            );

            Console.WriteLine(
                $"Luong co ban: {manager.BaseSalary:N0} VND"
            );

            Console.WriteLine(
                $"Phu cap: {manager.ResponsibilityAllowance:N0} VND"
            );

            Console.WriteLine(
                $"Thu nhap: {manager.CalculateIncome():N0} VND"
            );
        }
    }
}