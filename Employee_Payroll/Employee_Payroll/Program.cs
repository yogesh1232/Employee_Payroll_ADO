using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Payroll
{
    internal class Program
    {
        public static void EmployeePayroll()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            //employeeRepository.GetEmployeeRecords();
            EmployeeModel Model = new EmployeeModel();
            Console.WriteLine("Add employee in database");
            Model.EmployeeId = 4;
            Model.EmployeeName = "Jayesh";
            Model.PhoneNumber = 34543787519;
            Model.Address = "Bagpat";
            Model.Department = "Engineer";
            Model.Gender = 'M';
            Model.BasicPay = 20000;
            Model.Deductions = 100;
            Model.TaxablePay = 19900;
            Model.IncomeTax = 0;
            Model.StartDate = DateTime.Now;
            Model.NetPay = 19900;
            employeeRepository.AddEmployee(Model);
            Console.WriteLine("Update basic salary");
            Model.EmployeeName = "Kavita";
            Model.BasicPay = 35000;
            employeeRepository.UpdateBasicPay(Model);
            Console.WriteLine("Update basic salary using prepared statement");
            Model.EmployeeName = "Sangita";
            Model.BasicPay = 45000;
            employeeRepository.UpdateBasicPayByPreparedStatement(Model);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll program");
            EmployeePayroll();
            Console.ReadLine();
        }
    }
}