using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Payroll
{
    public class EmployeeRepository
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=payroll_service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        readonly SqlConnection connection = new SqlConnection(connectionString);
        public void GetEmployeeRecords()
        {
            try
            {
                EmployeeModel model = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"SELECT * from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    this.connection.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            model.EmployeeId = sdr.GetInt32(0);
                            model.EmployeeName = sdr.GetString(1);
                            model.BasicPay = sdr.GetInt32(2);
                            model.StartDate = sdr.GetDateTime(3);
                            model.Gender = sdr.GetString(4);
                            model.PhoneNumber = sdr.GetInt64(5);
                            model.Address = sdr.GetString(6);
                            model.Department = sdr.GetString(7);
                            model.Deductions = sdr.GetInt32(8);
                            model.TaxablePay = sdr.GetInt32(9);
                            model.IncomeTax = sdr.GetInt32(10);
                            model.NetPay = sdr.GetInt32(11);
                            //Print Record on Console
                            Console.WriteLine("{0},{1},{2},{3},{4},{5}", model.EmployeeId, model.EmployeeName, model.Gender, model.Department, model.Address, model.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                        Console.WriteLine("No Records in Database");
                    sdr.Close();
                    this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
