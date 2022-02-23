﻿using System;
using System.Collections.Generic;
using System.Data;
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
                            model.Gender = Convert.ToChar(sdr.GetString(4));
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
        public void AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spAddEmployeeDetails", this.connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", model.EmployeeName);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Startdate", model.StartDate);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@phoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@Taxable_Pay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Income_Tax", model.IncomeTax);
                    command.Parameters.AddWithValue("@Net_Pay", model.NetPay);


                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    this.connection.Close();
                    if (result != 0)
                        Console.WriteLine("Data inserted in DataBase");
                    else
                        Console.WriteLine(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void UpdateBasicPay(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spUpdateEmp", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", model.EmployeeName);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);

                    this.connection.Open();
                    //Executes Sql statement to Update in DB
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                        Console.WriteLine("Data updated in DB");
                    else
                        Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public void UpdateBasicPayByPreparedStatement(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("update employee_payroll set Base_Pay = @BasicPay where Name = @Name;", this.connection);
                    command.Prepare();
                    command.Parameters.AddWithValue("@Name", model.EmployeeName);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    //Open Connection of Database
                    this.connection.Open();
                    //Executes Sql statement to Update in Db.
                    var rows = command.ExecuteNonQuery();
                    //Close Connection of database
                    this.connection.Close();
                    if (rows != 0)
                        Console.WriteLine("Updated in Db");
                    else
                        Console.WriteLine(rows);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}

