using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Reflection;

namespace Assignment6AirlineReservation
{
    class clsDataAccess
    {
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string sConnectionString;

        /// <summary>
        /// Constructor that sets the connection string to the database
        /// </summary>
		public clsDataAccess()
        {
            sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ReservationSystem.mdb";
        }

        /// <summary>
        /// This method retrieves the flight information from the Database.
        /// </summary>
        /// <param name="iRet"></param>
        /// <returns></returns>
        public DataSet RetrieveChooseFlight(ref int iRet)
        {
            try
            {
                DataSet ds = new DataSet();
                //Should probably not have SQL statements behind the UI
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";


                //This should probably be in a new class.  Would be nice if this new class
                //returned a list of Flight objects that was then bound to the combo box
                //Also should show the flight number and aircraft type together
                ds = ExecuteSQLStatement(sSQL, ref iRet);

                return ds;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method retrieves the passenger informatinon from the Database.
        /// </summary>
        /// <param name="iRet"></param>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public DataSet RetrieveChoosePassenger(ref int iRet, string sFlightID)
        {
            try
            {
                DataSet ds = new DataSet();
                string sSQL = "SELECT p.Passenger_ID, First_Name, Last_Name, l.Seat_Number " +
                              "FROM Passenger p, Flight_Passenger_Link l " +
                              "WHERE p.Passenger_ID = l.Passenger_ID AND " +
                              "Flight_ID = " + sFlightID;

                ds = ExecuteSQLStatement(sSQL, ref iRet);
                return ds;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method deletes passengers from the Database.
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sCurrentPassID"></param>
        public void DeletePassenger(string sFlightID, string sCurrentPassID)
        {
            try
            {
                string sSQL = "DELETE FROM Flight_Passenger_Link WHERE Passenger_ID = " + sCurrentPassID +
                              " AND Flight_ID = " + sFlightID;

                ExecuteNonQuery(sSQL);

                sSQL = "DELETE FROM Passenger WHERE Passenger_ID = " + sCurrentPassID;

                ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method retrieves a specific seat number from the database.
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sCurrentPassID"></param>
        /// <returns></returns>
        public string RetrieveSeatNumber(string sFlightID, string sCurrentPassID)
        {
            try
            {
                string sSQL = "SELECT Seat_Number FROM Flight_Passenger_Link WHERE " +
                              "Passenger_ID = " + sCurrentPassID +
                              " AND Flight_ID = " + sFlightID;

                return ExecuteScalarSQL(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method retrieves the seats that have already been taken from the database.
        /// </summary>
        /// <param name="iRet"></param>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public DataSet RetrieveChosenSeats(ref int iRet, string sFlightID)
        {
            try
            {
                DataSet ds = new DataSet();
                string sSQL = "SELECT Seat_Number " +
                              "FROM Flight_Passenger_Link " +
                              "WHERE Flight_ID = " + sFlightID;

                ds = ExecuteSQLStatement(sSQL, ref iRet);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method adds a passenger to the database.
        /// </summary>
        /// <param name="sFirst"></param>
        /// <param name="sLast"></param>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public string AddPassenger(string sFirst, string sLast, string sFlightID)
        {
            try
            {
                string sSQL = "INSERT INTO Passenger " +
                              "(First_Name, Last_Name) " +
                              "VALUES " +
                              "('" + sFirst + "', '" + sLast + "')";

                ExecuteNonQuery(sSQL);

                sSQL = "SELECT Passenger_ID FROM Passenger WHERE First_Name = '" + sFirst + "' AND Last_Name = '" + sLast + "'";

                string ID = ExecuteScalarSQL(sSQL);


                sSQL = "INSERT INTO Flight_Passenger_Link " +
                       "(Flight_ID, Passenger_ID) " +
                       "VALUES" +
                       "('" + sFlightID + "', '" + ID + "')";

                ExecuteNonQuery(sSQL);

                return ID;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method changes the currently selected passengers seat in the database.
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="seat"></param>
        /// <param name="ID"></param>
        public void ChangeSeat(int sFlightID, string seat, int ID)
        {
            try
            {
                string sSQL = "UPDATE Flight_Passenger_Link SET Seat_Number = '" + seat + "' WHERE Flight_ID = " + sFlightID + " AND " +
                              "Passenger_ID = " + ID + "";

                ExecuteNonQuery(sSQL);

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statement that is passed in and executes it.  The resulting values
        /// are returned in a DataSet.  The number of rows returned from the query will be put into
        /// the reference parameter iRetVal.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <param name="iRetVal">Reference parameter that returns the number of selected rows.</param>
        /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
        public DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal)
        {
            try
            {
                //Create a new DataSet
                DataSet ds = new DataSet();

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(ds);
                    }
                }

                //Set the number of values returned
                iRetVal = ds.Tables[0].Rows.Count;

                //return the DataSet
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statement that is passed in and executes it.  The resulting single 
        /// value is returned.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns a string from the scalar SQL statement.</returns>
		public string ExecuteScalarSQL(string sSQL)
        {
            try
            {
                //Holds the return value
                object obj;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Execute the scalar SQL statement
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }

                //See if the object is null
                if (obj == null)
                {
                    //Return a blank
                    return "";
                }
                else
                {
                    //Return the value
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statement that is a non query and executes it.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns the number of rows affected by the SQL statement.</returns>
        public int ExecuteNonQuery(string sSQL)
        {
            try
            {
                //Number of rows affected
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    iNumRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return iNumRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
