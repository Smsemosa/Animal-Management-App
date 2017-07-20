using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace DatabaseAccessLayer
{

    public class DataAccess :iConnect
    {
        private static string connectionString;
        private static SqlConnection conn;


        #region Properties

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public SqlConnection Connection
        {
            get { return conn; }
            set { conn = value; }
        }

        #endregion

        #region Constructor

        //singleton instance 
        private static DataAccess instanceDB = new DataAccess();
        public static DataAccess getInstance()
        {
            return instanceDB;
        }
        public  DataAccess()
        {
            try
            {//LAPTOP-4D9443AI
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn = new SqlConnection(connectionString);
               // conn.Open();
               // string states = conn.State.ToString();
            }
            
            catch (Exception e)
            {
                
            }

        }

        #endregion

        #region  Connection Methods

        public void OpenConnection()
        {
            if ((conn.State != ConnectionState.Open) || (conn.State == ConnectionState.Broken))
            {
                conn.Open();
            }
        }

        public void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }


        public bool TestConnection()
        {
            bool stat = false;
            if (conn.State.ToString() == "Open")
            {
                stat = true;
                return true;
            }
            else if(conn.State.ToString() == "Closed")
            {
                stat = false;
                return false;
            }

            return stat;
        }

        #endregion
        #region Access Methods

       
        private static DataTable dataTable;
        private SqlDataAdapter adapter;
        private static SqlCommand command;





        public DataTable Select(string Query)
        {
            //EDITED
            dataTable = new DataTable();
            //END EDITED
            
            conn = new SqlConnection(connectionString);

            if (TestConnection())
            {
                return dataTable;
            }
            else
            {
                OpenConnection();
            }



            using (adapter = new SqlDataAdapter(Query, conn))
            {
                adapter.Fill(dataTable);
            }

            CloseConnection();

            return dataTable;
        }

        public bool Insert(string tableName, string[] Columns, string[] Values)
        {


            string Query = "";
            return ExecuteQuery(Query);

        }

      
    

        //method to execute procedure that returns data table
        public  DataTable ExecuteDataTableProcedure(string procName, string[] Variables, string[] Values,DataAccess connect)
        {
            //store data from database
            dataTable = new DataTable();
            

               // Get From the Procedure Created In The Database.
               SqlParameter ReturnValue = new SqlParameter(" ", SqlDbType.Int);
               ReturnValue.Direction = ParameterDirection.ReturnValue;

               //test connection state
            
                       connect.OpenConnection();
              
             


                       command = new SqlCommand(procName, conn);
                       command.CommandType = CommandType.StoredProcedure;

                        //this refers to the parameter of the procedure in the database
                        if (Variables != null)
                        {
                            for (int i = 0; i < Variables.Length; i++)
                            {
                                command.Parameters.AddWithValue(Variables[i], Values[i]);
                            }

                          //converts data reader data into datatable 
                          dataTable.Load(command.ExecuteReader());
                          connect.CloseConnection();
                        }
                    
               

            return dataTable;
        }


        public  object ExecuteOneValReturnProcedure<T>(string procName, string[] Variables, string[] Values,string returnString ,object datatype, DataAccess connect)
        {

            object val = String.Empty;

            // Get From the Procedure Created In The Database.
            SqlParameter ReturnValue = new SqlParameter(" ", SqlDbType.Int);
            ReturnValue.Direction = ParameterDirection.ReturnValue;
            command = new SqlCommand(procName, conn);
            command.CommandType = CommandType.StoredProcedure;
          

            //this refers to the parameter of the procedure in the database
            if ((Variables.Length !=  null) &&( Variables.Length != 0))
            {
                //add parameters 
                for (int i = 0; i < Variables.Length; i++)
                {
                    command.Parameters.AddWithValue(Variables[i], Values[i]);
                }

                connect.OpenConnection();
                //return type from database 
                SqlParameter parameter = new SqlParameter(returnString, datatype);
                parameter.Direction = ParameterDirection.ReturnValue;

                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
                connect.CloseConnection();

                val = parameter.Value.ToString();
                return val;
               
            }
            if (Values != null)
            {
                if (Values[0].Equals("Sequence"))
                {
                    //connect.OpenConnection();
                    //return type from database 
                    SqlParameter parameter = new SqlParameter(returnString, datatype);
                    parameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(parameter);
                    command.ExecuteNonQuery();
                    connect.CloseConnection();
                    val = parameter.Value.ToString();

                    return val;
                }
            }
            return val;

        }






        public bool ExecuteQuery(String Query)
        {
            int AffectedRows = 0;

            try
            {
                conn = new SqlConnection(connectionString);

                using (command = new SqlCommand(Query, conn))
                {
                    AffectedRows = command.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
            }
            finally
            {

                CloseConnection();
            }
            return AffectedRows > 0;
        }

        //public int ExecuteProcedure(string procName, string[] Variables, string[] Values, bool hasReturnValue)
        //{
        //    connection = new SqlConnection(connectionString);

        //    int AffectedRows = 0;

        //    if (!TestConnection()) return AffectedRows;

        //    // Get From the Procedure Created In The Database.
        //    SqlParameter ReturnValue = new SqlParameter(" ", SqlDbType.Int);
        //    ReturnValue.Direction = ParameterDirection.ReturnValue;

        //    OpenConnection();

        //    using (command = new SqlCommand(procName, connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;

        //        if (Variables != null)
        //        {
        //            for (int i = 0; i < Variables.Length; i++)
        //            {
        //                command.Parameters.Add(new SqlParameter(Variables[i], Values[i]));
        //            }
        //        }

        //        if (hasReturnValue)
        //            command.Parameters.Add(ReturnValue);

        //        AffectedRows = command.ExecuteNonQuery();
        //    }

        //    CloseConnection();



        //    if (ReturnValue.ToString() == string.Empty)
        //    {
        //        Console.WriteLine(ReturnValue.ToString());
        //    }

        //    if (hasReturnValue)
        //    {
        //        string[] ac = new string[1];
        //        ac[0] = ReturnValue.ToString();

        //        if (ac[0] != " ")
        //        {
        //            return int.Parse(ReturnValue.ToString());
        //        }

        //    }
        //    return AffectedRows;
        //}

        #endregion
    }

}
