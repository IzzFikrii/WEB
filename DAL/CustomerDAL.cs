using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_P04_5.Models;

namespace WEB_P04_5.DAL
{
    public class CustomerDAL
    {
        private IConfiguration Configuration { get; }
  

        private SqlConnection conn;
 
        //Constructor
        public CustomerDAL()
        {
            //Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
            "ZZFashionConnectionString");
            //Instantiate a SqlConnection object with the
            //Connection String read.
            conn = new SqlConnection(strConn);
        }

        public Customer GetCustomer(string loginID, string password)
        {

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $"SELECT * FROM Customer WHERE MemberID = '{loginID}' AND MPassword = '{password}'";

            conn.Open();


            SqlDataReader reader = cmd.ExecuteReader();
            Customer c = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    c = new Customer
                    {
                        MemberID = reader.GetString(0),
                        MName = reader.GetString(1),
                        MGender = reader.GetString(2)[0],
                        MBirthDate = reader.GetDateTime(3),
                        MAddress = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        MCountry = reader.GetString(5),
                        MTelNo = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        MEmailAddr = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        MPassword = reader.GetString(8)

                    };

                }
            }

            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return c;
        }

        public List<Customer> GetAllCustomers(string MemberID)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = $"SELECT * FROM Customer WHERE MemberID = @memberid";
            cmd.Parameters.AddWithValue("@memberid", MemberID);
            List<Customer> CustomerList = new List<Customer>();
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list

            while (reader.Read())
            {
                CustomerList.Add(new Customer
                {
                    MemberID = reader.GetString(0), //0: 1st column
                    MName = reader.GetString(1), //1: 2nd column                           //Get the first character of a string
                    MGender = reader.GetString(2)[0], //2: 3rd column
                    MBirthDate = reader.GetDateTime(3), //3: 4th column
                    MAddress = reader.IsDBNull(4) ? "" : reader.GetString(4), //5: 6th column
                    MCountry = reader.GetString(5), //6: 7th column
                    MTelNo = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    MEmailAddr = reader.IsDBNull(7) ? "" : reader.GetString(7), //9: 10th column
                    MPassword = reader.GetString(8) //11: 12th column
                                                    //7 - 8th column, assign Branch Id,
                                                    //if null value in db, assign integer null value
                });

            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return CustomerList;
        }

        

        public bool IsEmailExist(string MEmailAddr, string MemberID)
        {
            bool emailFound = false;
            //Create a SqlCommand object and specify the SQL statement
            //to get a staff record with the email address to be validated
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT MemberID FROM Customer
            WHERE MEmailAddr=@selectedEmail";
            cmd.Parameters.AddWithValue("@selectedEmail", MEmailAddr);
            //Open a database connection and execute the SQL statement
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            { //Records found
                while (reader.Read())
                {
                    if (reader.GetString(0) != MemberID)
                        //The email address is used by another staff
                        emailFound = true;
                }
            }
            else
            { //No record
                emailFound = false; // The email address given does not exist
            }
            reader.Close();
            conn.Close();
            return emailFound;
        }

        public bool IsTelNoExist(string MTelNo, string MemberID)
        {
            bool TelNoFound = false;

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT MemberID FROM Customer WHERE MTelNo=@selectedTelNo";
            cmd.Parameters.AddWithValue("@selectedTelNo", MTelNo);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetString(0) != MemberID)
                        TelNoFound = true;
                }
            }
            else
            {
                TelNoFound = false;
            }
            reader.Close();
            conn.Close();
            return TelNoFound;
        }


        public Customer GetDetails(string loginID)
        {
            Customer customer = new Customer();

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = $"SELECT * FROM Customer WHERE MemberID = '{loginID}'";

            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “MemberId”.


            //Open a database connection
            conn.Open();

            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill staff object with values from the data reader
                    customer.MemberID = reader.IsDBNull(0) ? "" : reader.GetString(0);
                    customer.MName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    customer.MGender = !reader.IsDBNull(2) ? reader.GetString(2)[0] : (char)0;
                    customer.MBirthDate = (DateTime)(!reader.IsDBNull(3) ? reader.GetDateTime(3) : (DateTime?)null);
                    customer.MAddress = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    customer.MCountry = reader.IsDBNull(5) ? "" : reader.GetString(5);
                    customer.MTelNo = reader.IsDBNull(6) ? "" : reader.GetString(6);
                    customer.MEmailAddr = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    customer.MPassword = reader.IsDBNull(8) ? "" : reader.GetString(8);
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();

            return customer;
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public  void Update(Customer customer, HttpContext httpContext)
        {
            //[MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]
            string username = httpContext.Session.GetString("MName");
            SqlCommand command = conn.CreateCommand();
            command.CommandText = @"UPDATE Customer SET MemberID=@MemberID, MName=@MName, MGender=@MGender, MBirthDate=@MBirthDate, MAddress=@MAddress, MCountry=@MCountry, MTelNo=@MTelNo, MEmailAddr=@MEmailAddr WHERE MName=@TargetUsername";
            //Tell SQL what data you want to plug in
            conn.Open();
            command.Parameters.AddWithValue("@MemberID", customer.MemberID);
            command.Parameters.AddWithValue("@MName", customer.MName);
            command.Parameters.AddWithValue("@MGender", customer.MGender);
            command.Parameters.AddWithValue("@MBirthDate", customer.MBirthDate);
            command.Parameters.AddWithValue("@MAddress", customer.MAddress);
            command.Parameters.AddWithValue("@MCountry", customer.MCountry);
            command.Parameters.AddWithValue("@MTelNo", customer.MTelNo);
            command.Parameters.AddWithValue("@MEmailAddr", customer.MEmailAddr);
            //Tell SQL which customer record you want it to be updated
            command.Parameters.AddWithValue("@TargetUsername", username);
            command.ExecuteNonQuery();
            conn.Close();
        }
        /*
        public int Update(IFormCollection FormData)
        {
            string EmailAddress = FormData["EmailAddress"].ToString();
            string TelNo = FormData["TelNo"].ToString();
            string Password = FormData["Password"].ToString();
            string Address = FormData["Address"].ToString();

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = (@"UPDATE Customer SET MName = @name,MEmailAddr = @email,MPassword =@password, MGender = @gender, MBirthdate = @Birthdate, MAddress = @Address, MCountry = @Country , MTelNo = @TelNo WHERE MemberID = @selectedMemberID");



            cmd.Parameters.AddWithValue("@email", EmailAddress);
            cmd.Parameters.AddWithValue("@password", Password);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@TelNo", TelNo);

            /*cmd.Parameters.AddWithValue("@name", customer.MName);
            cmd.Parameters.AddWithValue("@gender", customer.MGender);
            cmd.Parameters.AddWithValue("@Birthdate", customer.MBirthDate);
            cmd.Parameters.AddWithValue("@Address", customer.MAddress);
            cmd.Parameters.AddWithValue("@Country", customer.MCountry);
            cmd.Parameters.AddWithValue("@TelNo", customer.MTelNo);
            cmd.Parameters.AddWithValue("selectedMemberID", customer.MemberID); 
            /*$"UPDATE Customer SET MTelNo='{customer.MTelNo}', " +
            $"MEmailAddr='{customer.MEmailAddr}', " +
            $"MPassword='{customer.MPassword}', " +
            $"MAddress='{customer.MAddress}', " +
            $"MemberID='{customer.MemberID}' " +
            $"WHERE MemberID='{customer.MemberID}'"; */


        //Open a database connection
     

            
            //ExecuteNonQuery is used for UPDATE and DELETE
           

            //Close the database connection
            

        }
    }
