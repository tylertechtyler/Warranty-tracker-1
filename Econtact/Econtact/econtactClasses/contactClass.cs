using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact.econtactClasses
{
    class contactClass
    {
        //Getter Setter Properties 
        //Acts as Data Carrier in Our Application
        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public string Computer { get; set; }
        

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //SElecting Data from Database
        public DataTable Select()
        {
            ///Step 1: Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //Step 2: Writing SQL Query
                string sql = "SELECT * FROM tbl_contact";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Inserting DAta into Database
        public bool Insert (contactClass c)
        {
            //Creating a default return type and setting its value to false
            bool isSuccess = false;

            //STep 1: Connect DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //STep 2: Create a SQL Query to insert DAta
                string sql = "INSERT INTO tbl_contact (Name, Number, Date, Computer) VALUES (@Name, @Number, @Date, @Computer)";
                //Creating SQL Command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create Parameters to add data
                cmd.Parameters.AddWithValue("@Name", c.Name);
                cmd.Parameters.AddWithValue("@Number", c.Number);
                cmd.Parameters.AddWithValue("@Date", c.Date);
                cmd.Parameters.AddWithValue("@Computer", c.Computer);
                

                //Connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //Method to update data in database from our application
        public bool Update(contactClass c)
        {
            //Create a default return type and set its default value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL to update data in our Database
                string sql = "UPDATE tbl_contact SET Name=@Name, Number=@Number, Date=@Date, Computer=@Computer WHERE ContactID=@ContactID";

                //Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create Parameters to add value
                cmd.Parameters.AddWithValue("@Name", c.Name);
                cmd.Parameters.AddWithValue("@Number", c.Number);
                cmd.Parameters.AddWithValue("@Date", c.Date);
                cmd.Parameters.AddWithValue("@Computer", c.Computer);
                
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);
                //Open DAtabase Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //if the query runs sucessfully then the value of rows will be greater than zero else its value will be zero
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //Method to Delete Data from DAtabase
        public bool Delete(contactClass c)
        {
            //Create a default return value and set its value to false
            bool isSuccess = false;
            //Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL To Delte DAta
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                //Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //Open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query run sucessfully then the value of rows is greater than zero else its value is 0
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                //Close Connection
                conn.Close();
            }
            return isSuccess;
        }
        
    }
}
