using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlIndust_details
{
    class Program
    {

        static void Main(string[] args)
        {
            new Program().CreateTable();
        }

        public void CreateTable()
        {
             //   lable1:
            SqlConnection con = null;
            try
            {

                    lable1:
                // Creating Connection  
                con = new SqlConnection("data source=.; database=My_Database; integrated security=SSPI");
                // writing sql query 
                
                Console.WriteLine("Enter Company name");
                string name = Console.ReadLine();
                SqlCommand cm = new SqlCommand("SELECT Country_id, s_w_indust_name,h_w_comp_name FROM Industr_details where   s_w_indust_name='"+name+"'  ", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();

                //  Console.WriteLine(sdr.HasRows);

                // Iterating Data  
                if (!sdr.HasRows)
                {
                    Console.WriteLine("no data found");
                    Console.WriteLine("if want to continue again write y or close write n <y/n>");
                    char ch = char.Parse(Console.ReadLine());
                    if (ch.Equals('y'))
                    {
                        Console.Clear();
                        cm.Dispose();
                        sdr.Close();
                        con.Close();
                        
                       
                        goto lable1;
                    }

                }
                else
                {
                 //   sdr = cm.ExecuteReader();
                    while (sdr.Read())
                    {

                        Console.WriteLine(sdr[0] + " " + sdr[1] + " " + sdr[2]); // Displaying Record  
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
            }
            // Closing the connection  
            finally
            {
                Console.ReadLine();
                con.Close();
            }
        }
    }
}

