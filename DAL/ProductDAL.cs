using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using Web_P04_5.Models;

//testing by eeann

namespace Web_P04_5.DAL
{
    public class ProductDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;
        //Constructor
        public ProductDAL()
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
        public List<Product> GetAllProduct()
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SELECT SQL statement
            cmd.CommandText = @"SELECT * FROM Product ORDER BY ProductID";
            //Open a database connection
            conn.Open();
            //Execute the SELECT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();

            //Read all records until the end, save data into a staff list
            List<Product> productList = new List<Product>();
            
            while (reader.Read())
            {
                productList.Add(
                new Product
                {
                    ProductId = reader.GetInt32(0), //0: 1st column
                    ProductTitle = reader.GetString(1), //1: 2nd column
                    ProductImage = !reader.IsDBNull(2) ? //2: 3nd column
                                   reader.GetString(2) : null,
                    Price = !reader.IsDBNull(3) ? 
                            reader.GetDecimal(3) : (Decimal)0.00, //3: 4nd column
                    EffectiveDate = reader.GetDateTime(4), //4: 5th column
                    //Get the first character of a string
                    Obsolete = reader.GetString(5)[0], //5: 6th column
                }
                );
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();
            return productList;
        }

        
        public Product GetDetails(int productId)
        {
            Product product = new Product();

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM Product
                                WHERE ProductID = @selectedProductID";

            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectedProductID", productId);

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
                    product.ProductId = productId;
                    product.ProductTitle = !reader.IsDBNull(1) ? 
                    reader.GetString(1) : null;
                    product.ProductImage = !reader.IsDBNull(2) ?
                    reader.GetString(2) : null;
                    product.Price = !reader.IsDBNull(3) ?
                    reader.GetDecimal(3) : (Decimal)0.00;
                    product.EffectiveDate = (DateTime)(!reader.IsDBNull(4) ?
                    reader.GetDateTime(4) : (DateTime?)null);
                    product.Obsolete = !reader.IsDBNull(5) ?
                    reader.GetString(5)[0] : (char)0;
                }
            }
            //Close data reader
            reader.Close();
            //Close database connection
            conn.Close();

            return product;
        }

        public int Add(Product product)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an INSERT SQL statement which will
            //return the auto-generated StaffID after insertion
            cmd.CommandText = @"INSERT INTO Product (ProductTitle, ProductImage, Price, EffectiveDate, Obsolete)
                                OUTPUT INSERTED.ProductID
                                VALUES(@title, @image, @price, @date, @obsolete)";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@title", product.ProductTitle);
            if (product.ProductImage != null)
                // Image is uploaded
                cmd.Parameters.AddWithValue("@image", product.ProductImage);
            else // No Image is uploaded
                cmd.Parameters.AddWithValue("@image", DBNull.Value);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@date", product.EffectiveDate);
            cmd.Parameters.AddWithValue("@obsolete", product.Obsolete);
            //A connection to database must be opened before any operations made.
            conn.Open();
            //ExecuteScalar is used to retrieve the auto-generated
            //StaffID after executing the INSERT SQL statement
            product.ProductId = (int)cmd.ExecuteScalar();
            //A connection should be closed after operations.
            conn.Close();
            //Return id when no error occurs.
            return product.ProductId;
        }
        
        // Return number of row updated
        public int Update(Product product)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Product SET 
                                ProductTitle=@title,
                                ProductImage=@image,         
                                Price = @price,
                                Obsolete = @obsolete
                                WHERE ProductID = @selectedProductID";

            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@title", product.ProductTitle);
            if (product.ProductImage != null)
                // Image is uploaded
                cmd.Parameters.AddWithValue("@image", product.ProductImage);
            else // No Image is uploaded
                cmd.Parameters.AddWithValue("@image", DBNull.Value);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@obsolete", product.Obsolete);
            cmd.Parameters.AddWithValue("@selectedProductID", product.ProductId);

            //Open a database connection
            conn.Open();

            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();

            //Close the database connection
            conn.Close();
            return count;
        }
        
        public int Delete(int productId)
        {
            //Instantiate a SqlCommand object, supply it with a DELETE SQL statement
            //to delete a staff record specified by a Staff ID
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM Product
              WHERE ProductID = @selectProductID";
            cmd.Parameters.AddWithValue("@selectProductID", productId);
            //Open a database connection
            conn.Open();
            int rowAffected = 0;
            //Execute the DELETE SQL to remove the staff record
            rowAffected += cmd.ExecuteNonQuery();
            //Close database connection
            conn.Close();
            //Return number of row of staff record updated or deleted
            return rowAffected;
        }
    }
}
