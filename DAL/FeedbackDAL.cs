using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using WEB_P04_5.Models;

namespace WEB_P04_5.DAL
{
    public class FeedbackDAL


    {
     
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public FeedbackDAL()
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

        public Feedback GetDetails(int FeedbackID)
        {
            Feedback feedback = new Feedback();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT * FROM Feedback WHERE FeedbackID = @selectedFeedbackID";

            cmd.Parameters.AddWithValue("selectedFeedbackID", FeedbackID);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while(reader.Read())
                {
                    feedback.FeedbackID = FeedbackID;
                    feedback.MemberID = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    feedback.DateTimePosted = (DateTime)(!reader.IsDBNull(2) ? reader.GetDateTime(2) : (DateTime?)null);
                    feedback.Title = reader.IsDBNull(3) ? reader.GetString(3) : null;
                    feedback.Text = reader.IsDBNull(4) ? reader.GetString(4) : null;
                    feedback.ImageFileName = reader.IsDBNull(5) ? reader.GetString(5) : null;
                }
            }
            reader.Close();
            conn.Close();
            return feedback;
        }
        public int Add(Feedback feedback)
        {
            SqlCommand cmd = conn.CreateCommand();


            cmd.CommandText = @"INSERT INTO Feedback (Title, Text, DateTimePosted, ImageFileName,
                              OUTPUT INSERTED.FeedbackID
                              VALUES(@title, @text, @datetime, @image)";

            cmd.Parameters.AddWithValue("@title", feedback.Title);
            cmd.Parameters.AddWithValue("@text", feedback.Text);
            cmd.Parameters.AddWithValue("@datetime", feedback.DateTimePosted);
            cmd.Parameters.AddWithValue("@image", feedback.ImageFileName);

            conn.Open();

            feedback.FeedbackID = (int)cmd.ExecuteScalar();

            conn.Close();

            return feedback.FeedbackID;
        }
        public List<Feedback> GetAllFeedback()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Feedback";
           
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            //Read all records until the end, save data into a staff list
            List<Feedback> FeedbackList = new List<Feedback>();
            while (reader.Read())
            {
                FeedbackList.Add(
                new Feedback
                {
                    FeedbackID = reader.GetInt32(0), //0: 1st column

                    //Get the first character of a string
                    MemberID = reader.GetString(1), //2: 2rd column
                    DateTimePosted = reader.GetDateTime(2), //3: 3rd column
                    Title = reader.GetString(3), //4: 4th column
                    Text = reader.IsDBNull(4) ? "" : reader.GetString(4), //5: 5th column
                    ImageFileName = reader.IsDBNull(5) ? "" : reader.GetString(5), //6: 6th column

                }
                ) ;
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return FeedbackList;
        }

      
    }
}

